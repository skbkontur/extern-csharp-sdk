using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Exceptions;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Contents
{
    internal class UserStreamWrapper : Stream
    {
        private readonly Stream stream;

        public UserStreamWrapper(Stream stream)
        {
            this.stream = stream;
        }

        public override bool CanRead => stream.CanRead;

        public override bool CanSeek => stream.CanSeek;

        public override bool CanWrite => false;

        public override long Length => stream.Length;

        public override long Position
        {
            get => stream.Position;
            set => stream.Position = value;
        }

        public override long Seek(long offset, SeekOrigin origin)
            => stream.Seek(offset, origin);

        public override int Read(byte[] buffer, int offset, int count)
        {
            try
            {
                return stream.Read(buffer, offset, count);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (StreamAlreadyUsedException)
            {
                throw;
            }
            catch (Exception error)
            {
                throw new UserStreamException($"Failed to read from user-provided stream of type '{stream.GetType().Name}'.", error);
            }
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            try
            {
                return await stream.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (StreamAlreadyUsedException)
            {
                throw;
            }
            catch (Exception error)
            {
                throw new UserStreamException($"Failed to read from user-provided stream of type '{stream.GetType().Name}'.", error);
            }
        }

        #region Not supported

        public override void Flush() 
            => throw new NotSupportedException();

        public override void SetLength(long value) 
            => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count) 
            => throw new NotSupportedException();

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state) 
            => throw new NotSupportedException();

        public override void EndWrite(IAsyncResult asyncResult) 
            => throw new NotSupportedException();

        public override Task FlushAsync(CancellationToken cancellationToken)
            => throw new NotSupportedException();

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            => throw new NotSupportedException();

        public override void WriteByte(byte value)
            => throw new NotSupportedException();

        #endregion
    }
}