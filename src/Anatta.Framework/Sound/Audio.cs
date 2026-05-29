using ManagedBass;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using Anatta.Framework.Storage;
using Anatta.Framework.Logging;
using OpenTK.Audio.OpenAL;
using OpenTK.Audio.OpenAL.ALC;


namespace Anatta.Framework.Sound {
    public static class Audio
    {
        public enum Bindings {
            Bass = 1,
            Al // Weird...
        }

        public static Bindings Binding = Bindings.Bass;
        
        private static bool _useOpenAl = false;
        static readonly ConcurrentDictionary<int, byte> _alSources = new();

        static bool _bassInitialized = false;

        internal static Logger logger = new("Audio");
        
        static readonly ConcurrentDictionary<int, GCHandle> PinnedBuffers = new(); 
        static readonly ConcurrentDictionary<int, int> AlBuffers = new();

        static bool openAlInitialized = false;
        static ALCDevice _device;
        static ALCContext _context;
        static int _currentMusicStream = 0;
        internal static byte[] LoadAudio(string resourceName)
        {
            return Resource.Load<byte[]>(resourceName);
        }
        public static double GetPositionSeconds(int stream) {
            if (stream == 0) return 0;

            long pos = Bass.ChannelGetPosition(stream);
            return Bass.ChannelBytes2Seconds(stream, pos);
        }
        public static double GetLengthSeconds(int stream) {
            long len = Bass.ChannelGetLength(stream);
            return Bass.ChannelBytes2Seconds(stream, len);
        }
        internal static int Play(byte[] data, bool loop = false)
        {
            switch (Binding) {
                case (Bindings.Bass):
                    _useOpenAl = false;
                    break;
                case (Bindings.Al):
                    _useOpenAl = true;
                    break;
            }
            if (data == null || data.Length == 0)
                throw new ArgumentException(nameof(data));
                    
            if (_currentMusicStream != 0)
                    Stop(_currentMusicStream);

            if (_useOpenAl)
                return _PlayAl(data, loop);
            else
                return PlayRg(data, loop);
        }

        private static int PlayRg(byte[] data, bool loop = false) {
            if (data == null || data.Length == 0) throw new ArgumentException(nameof(data));

            _InitBass();

            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();

            int stream = Bass.CreateStream(ptr, 0, data.Length, BassFlags.Prescan);
            if (stream == 0)
            {
                int err = (int)Bass.LastError;
                handle.Free();
                throw new InvalidOperationException($"audio failed to create stream: {err}");
            }

            PinnedBuffers[stream] = handle;
            
            if(!loop)
                Bass.ChannelSetSync(stream, SyncFlags.End, 0, (handleSync, channel, dataPtr, user) =>
                {
                    if (PinnedBuffers.TryRemove(channel, out var h) && h.IsAllocated)
                        h.Free();
                    Bass.StreamFree(channel);
                }, IntPtr.Zero);

            Bass.ChannelPlay(stream);
            return stream;
        }
        private static void _InitAL()
        {
            if (openAlInitialized)
                return;

            _device = ALC.OpenDevice((string?)null);
            if (_device == ALCDevice.Null)
                throw new InvalidOperationException("failed to open AL device.");

            _context = ALC.CreateContext(_device, (int[])null);
            if (_context == ALCContext.Null)
                throw new InvalidOperationException("failed to create AL context.");

            ALC.MakeContextCurrent(_context);

            openAlInitialized = true;
        }

        private static int _PlayAl(byte[] compressedData, bool loop)
        {
            _InitAL();

            int sampleRate;
            int channels;

            byte[] pcm = _DecodeToPcm(compressedData, out sampleRate, out channels);

            Format format =
                channels == 1 ? Format.FormatMono16 :
                channels == 2 ? Format.FormatStereo16 :
                throw new NotSupportedException("only mono or stereo supported");

            int buffer = AL.GenBuffer();
            int source = AL.GenSource();

            AL.BufferData(buffer, format, pcm, pcm.Length, sampleRate);
            AL.Sourcei(source, SourcePNameI.Buffer, buffer);
            AL.Sourcei(source, SourcePNameI.Looping, loop ? 1 : 0);
            AL.SourcePlay(source);
            _alSources.TryAdd(source, 0);

            AlBuffers[source] = buffer;

            return source;
        }

        internal static void Stop(int stream) {
            if (stream == 0) return;

            if (_alSources.TryRemove(stream, out _)) {
                AL.SourceStop(stream);
                if (AlBuffers.TryRemove(stream, out int buffer))
                    AL.DeleteBuffer(buffer);
                AL.DeleteSource(stream);
            }
            else {
                Bass.ChannelStop(stream);
                Bass.StreamFree(stream);
                if (PinnedBuffers.TryRemove(stream, out var h) && h.IsAllocated)
                    h.Free();
            }

            if (_currentMusicStream == stream)
                _currentMusicStream = 0;
        }

        internal static void Shutdown()
        {
            if (!openAlInitialized)
                return;

            ALC.MakeContextCurrent(ALCContext.Null);
            ALC.DestroyContext(_context);
            ALC.CloseDevice(_device);

            openAlInitialized = false;
        }
        private static byte[] _DecodeToPcm(byte[] compressedData, out int sampleRate, out int channels)
        {
            _InitBass();
            
            int stream = Bass.CreateStream(
                compressedData,
                0,
                compressedData.Length,
                BassFlags.Decode | BassFlags.Prescan);

            if (stream == 0)
                throw new Exception($"decode failed: {Bass.LastError}");

            var info = Bass.ChannelGetInfo(stream);
            sampleRate = info.Frequency;
            channels = info.Channels;

            long length = Bass.ChannelGetLength(stream);
            int totalBytes = (int)length;

            byte[] pcm = new byte[totalBytes];

            int bytesRead = Bass.ChannelGetData(stream, pcm, totalBytes);

            Bass.StreamFree(stream);

            return pcm;
        }
        static void _InitBass()
        {
            if (_bassInitialized) return;

            if (!Bass.Init())
            {
                if (!Bass.Init(0))
                    throw new InvalidOperationException($"Bass init failed: {Bass.LastError}");
            }

            _bassInitialized = true;
        }
    }
}