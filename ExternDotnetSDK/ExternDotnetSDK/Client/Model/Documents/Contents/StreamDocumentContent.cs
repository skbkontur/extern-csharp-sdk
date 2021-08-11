#nullable enable
using System;
using System.IO;
using System.Threading.Tasks;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Uploading;

namespace Kontur.Extern.Client.Model.Documents.Contents
{
    public class StreamDocumentContent : IDocumentContent
    {
        private readonly Stream stream;

        public StreamDocumentContent(Stream stream, string? contentType = null)
        {
            this.stream = stream;
            ContentType = contentType;
        }
        
        public string? ContentType { get; }

        public async Task<Signature> SignAsync(CertificateContent certificate, ICrypt crypt)
        {
            stream.Position = 0;
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
            return crypt.Sign(bytes, certificate.ToBytes());
        }

        public Task<Guid> UploadAsync(IContentService contentService, Guid accountId, TimeSpan? chunkUploadTimeout)
        {
            return stream.Length > contentService.UploadChunkSize 
                ? UploadByPartsAsync(contentService, accountId, contentService.UploadChunkSize, chunkUploadTimeout) 
                : contentService.UploadWholeContentAsync(accountId, stream, chunkUploadTimeout);
        }

        private async Task<Guid> UploadByPartsAsync(IContentService contentService, Guid accountId, int uploadChunkSize, TimeSpan? chunkUploadTimeout)
        {
            var streamLength = stream.Length;
            var start = 0L;
            Guid contentId;
            var buffer = new byte[uploadChunkSize];
            while (true)
            {
                var read = await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                if (read <= 0)
                    break;

                if (start == 0L)
                {
                    contentId = await contentService.UploadFirstChunkAsync(accountId, buffer, start, streamLength, chunkUploadTimeout).ConfigureAwait(false);
                }
                else
                {
                    await contentService.UploadIntermediateChunkAsync(accountId, contentId, buffer, start, streamLength, chunkUploadTimeout).ConfigureAwait(false);
                }

                start += buffer.Length;
            }

            return contentId;
        }
    }
}