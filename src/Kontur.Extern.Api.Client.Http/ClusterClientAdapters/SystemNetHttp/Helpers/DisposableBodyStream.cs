using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Helpers
{
    internal class DisposableBodyStream : Stream
    {
        private readonly Stream stream;
        private readonly DisposableState state;

        public DisposableBodyStream(Stream stream, DisposableState state)
        {
            this.stream = stream;
            this.state = state;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => stream.Length;

        public override long Position
        {
            get => stream.Position;
            set => throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
            => stream.Read(buffer, offset, count);

        public override int ReadByte()
            => stream.ReadByte();

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            => stream.ReadAsync(buffer, offset, count, cancellationToken);

#if NETCOREAPP2_1
        public override int Read(Span<byte> buffer) 
            => stream.Read(buffer);

        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
            => stream.ReadAsync(buffer, cancellationToken);
#endif

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object s)
            => stream.BeginRead(buffer, offset, count, callback, s);

        public override int EndRead(IAsyncResult asyncResult)
            => stream.EndRead(asyncResult);

        protected override void Dispose(bool disposing)
        {
            stream.Dispose();
            state.Dispose();
            base.Dispose(disposing);
        }

        #region Not supported

        public override void Flush()
            => throw new NotSupportedException();

        public override Task FlushAsync(CancellationToken cancellationToken)
            => throw new NotSupportedException();

        public override long Seek(long offset, SeekOrigin origin)
            => throw new NotSupportedException();

        public override void SetLength(long value)
            => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count)
            => throw new NotSupportedException();

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            => throw new NotSupportedException();

        public override void WriteByte(byte value)
            => throw new NotSupportedException();

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object _)
            => throw new NotSupportedException();

        public override void EndWrite(IAsyncResult asyncResult)
            => throw new NotSupportedException();

        #endregion
    }
}
