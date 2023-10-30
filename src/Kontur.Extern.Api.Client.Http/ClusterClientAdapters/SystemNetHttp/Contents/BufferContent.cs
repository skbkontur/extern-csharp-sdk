using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Helpers;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Contents
{
    internal class BufferContent : GenericContent
    {
        private readonly Content content;
        private readonly CancellationToken cancellationToken;

        public BufferContent(Content content, CancellationToken cancellationToken, bool setContentLengthHeader = true)
        {
            this.content = content;
            this.cancellationToken = cancellationToken;

            if (setContentLengthHeader)
                Headers.ContentLength = content.Length;
        }

        public override long? Length => content.Length;

        public override Stream AsStream => content.ToMemoryStream();

        public override Task Copy(Stream target)
        {
            // (epeshk): avoid passing large buffers that may end up cached in Socket instances.
            if (content.Length < Helpers.Constants.LOHObjectSizeThreshold)
                return target.WriteAsync(content.Buffer, content.Offset, content.Length, cancellationToken);

            return CopyWithIntermediateBuffer(target);
        }

        private async Task CopyWithIntermediateBuffer(Stream target)
        {
            using (BufferPool.Acquire(out var buffer))
            {
                var index = content.Offset;
                var end = content.Offset + content.Length;

                while (index < end)
                {
                    var size = Math.Min(buffer.Length, end - index);

                    Buffer.BlockCopy(content.Buffer, index, buffer, 0, size);

                    await target.WriteAsync(buffer, 0, size, cancellationToken).ConfigureAwait(false);

                    index += size;
                }
            }
        }
    }
}
