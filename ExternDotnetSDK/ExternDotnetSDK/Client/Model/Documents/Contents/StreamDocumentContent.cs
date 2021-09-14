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

        public Task<Guid> UploadAsync(IContentService contentService, Guid accountId, TimeSpan? chunkUploadTimeout) => 
            contentService.UploadContentByPartsAsync(accountId, stream, chunkUploadTimeout);
    }
}