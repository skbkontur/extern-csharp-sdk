#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Uploading;

namespace Kontur.Extern.Client.Model.Documents.Contents
{
    public class ByteDocumentContent : IDocumentContent
    {
        private readonly byte[] bytes;

        public ByteDocumentContent(byte[] bytes, string? contentType = null)
        {
            this.bytes = bytes;
            ContentType = contentType;
        }

        public string? ContentType { get; }

        public Task<Signature> SignAsync(CertificateContent certificate, ICrypt crypt)
        {
            Signature signature = crypt.Sign(bytes, certificate.ToBytes());
            return Task.FromResult(signature);
        }

        public Task<Guid> UploadAsync(IContentService contentService, Guid accountId, TimeSpan? chunkUploadTimeout) => 
            contentService.UploadWholeContentAsync(accountId, bytes, chunkUploadTimeout);
    }
}