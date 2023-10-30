using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Contents
{
    internal class CompositeStream : Stream
    {
        private readonly Queue<MemoryStream> streams;
        private long position;

        public CompositeStream(IEnumerable<MemoryStream> streams, long length)
        {
            this.streams = new Queue<MemoryStream>(streams);

            Length = length;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length { get; }

        public override long Position
        {
            get => position;
            set => throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count) =>
            ReadAsync(buffer, offset, count, CancellationToken.None).GetAwaiter().GetResult();

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var totalBytesRead = 0;

            while (totalBytesRead < count && streams.Count > 0)
            {
                var currentStream = streams.Peek();
                var remainingInCurrentStream = currentStream.Length - currentStream.Position;
                if (remainingInCurrentStream == 0L)
                {
                    streams.Dequeue();
                    continue;
                }

                var bytesRead = await currentStream.ReadAsync(buffer, offset + totalBytesRead, count - totalBytesRead, cancellationToken).ConfigureAwait(false);

                totalBytesRead += bytesRead;
                position += bytesRead;
            }

            return totalBytesRead;
        }

        #region Not supported

        public override void Flush()
            => throw new NotSupportedException();

        public override Task FlushAsync(CancellationToken cancellationToken)
            => throw new NotSupportedException();

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count)
            => throw new NotSupportedException();

        public override void WriteByte(byte value)
            => throw new NotSupportedException();

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            => throw new NotSupportedException();

        public override void EndWrite(IAsyncResult asyncResult)
            => throw new NotSupportedException();

        public override void SetLength(long value)
            => throw new NotSupportedException();

        public override long Seek(long offset, SeekOrigin origin)
            => throw new NotSupportedException();

        #endregion
    }
}
