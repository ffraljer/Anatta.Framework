using ManagedBass;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using Anatta.Framework.IO;
using OpenTK.Audio.OpenAL;
using OpenTK.Audio.OpenAL.ALC;

namespace Anatta.Framework.Sound {
    public static class Audio
    {
        public enum Bindings {
            Bass = 1,
            AL
        }

        public static Bindings Binding = Bindings.Bass;
        
        private static bool UseOpenAL = false;
        
        static readonly ConcurrentDictionary<int, GCHandle> PinnedBuffers = new(); 
        static readonly ConcurrentDictionary<int, int> OpenALBuffers = new();

        static bool openAlInitialized = false;
        static ALCDevice _device;
        static ALCContext _context;

        internal static byte[] LoadAudio(string resourceName)
        {
            return Resource.Load<byte[]>(resourceName);
        }

        internal static int Play(byte[] data)
        {
            switch (Binding) {
                case (Bindings.Bass):
                    UseOpenAL = false;
                    break;
                case (Bindings.AL):
                    UseOpenAL = true;
                    break;
            }
            if (data == null || data.Length == 0)
                throw new ArgumentException(nameof(data));

            if (UseOpenAL)
                return PlayAL(data);
            else
                return PlayRg(data);
        }

        private static int PlayRg(byte[] data) {
            if (data == null || data.Length == 0) throw new ArgumentException(nameof(data));

            if (!Bass.Init())
            {
                if (!Bass.Init(0))
                    throw new InvalidOperationException($"init fail: {Bass.LastError}");
            }

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

            Bass.ChannelSetSync(stream, SyncFlags.End, 0, (handleSync, channel, dataPtr, user) =>
            {
                if (PinnedBuffers.TryRemove(channel, out var h) && h.IsAllocated)
                    h.Free();
                Bass.StreamFree(channel);
            }, IntPtr.Zero);

            Bass.ChannelPlay(stream);
            return stream;
        }
        private static void initAL()
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

        private static int PlayAL(byte[] compressedData)
        {
            initAL();

            int sampleRate;
            int channels;

            byte[] pcm = decodetopcm(compressedData, out sampleRate, out channels);

            Format format =
                channels == 1 ? Format.FormatMono16 :
                channels == 2 ? Format.FormatStereo16 :
                throw new NotSupportedException("only mono or stereo supported");

            int buffer = AL.GenBuffer();
            int source = AL.GenSource();

            AL.BufferData(buffer, format, pcm, pcm.Length, sampleRate);
            AL.Sourcei(source, SourcePNameI.Buffer, buffer);
            AL.SourcePlay(source);

            OpenALBuffers[source] = buffer;

            return source;
        }

        internal static void Stop(int stream)
        {
            if (stream == 0) return;

            Bass.ChannelStop(stream);
            Bass.StreamFree(stream);

            if (PinnedBuffers.TryRemove(stream, out var h) && h.IsAllocated)
                h.Free();
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
        static byte[] decodetopcm(byte[] compressedData, out int sampleRate, out int channels)
        {
            initBass();
            
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
        static void initBass()
        {
            if (Bass.Init()) return;

            if (!Bass.Init())
            {
                if (!Bass.Init(0))
                    throw new InvalidOperationException($"Bass init failed: {Bass.LastError}");
            }
        }
    }
}