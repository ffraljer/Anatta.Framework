using ManagedBass;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using Anatta.Framework.IO;

namespace Anatta.Framework.Sound {
    public static class Audio
    {
        static readonly ConcurrentDictionary<int, GCHandle> PinnedBuffers = new(); 

        public static byte[] LoadAudio(string resourceName)
        {
            return Resource.Load<byte[]>(resourceName);
        }

        public static int Play(byte[] data)
        {
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

        public static void Stop(int stream)
        {
            if (stream == 0) return;

            Bass.ChannelStop(stream);
            Bass.StreamFree(stream);

            if (PinnedBuffers.TryRemove(stream, out var h) && h.IsAllocated)
                h.Free();
        }
    }
}