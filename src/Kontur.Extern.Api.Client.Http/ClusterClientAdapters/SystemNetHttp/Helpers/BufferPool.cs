using System;
using System.Buffers;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Helpers
{
    internal static class BufferPool
    {
        public const int BufferSize = 16 * 1024;

        public static Releaser Acquire(out byte[] buffer)
        {
            return new Releaser(buffer = ArrayPool<byte>.Shared.Rent(BufferSize));
        }

        public struct Releaser : IDisposable
        {
            private readonly byte[] buffer;

            public Releaser(byte[] buffer)
            {
                this.buffer = buffer;
            }

            public void Dispose()
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }
}
