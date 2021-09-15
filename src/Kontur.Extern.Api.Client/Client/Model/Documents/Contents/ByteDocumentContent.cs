using System;
using System.Text;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Uploading;
using Kontur.Extern.Api.Client.Cryptography;
using Kontur.Extern.Api.Client.Http.Constants;

namespace Kontur.Extern.Api.Client.Model.Documents.Contents
{
    public class ByteDocumentContent : IDocumentContent
    {
        private readonly byte[] bytes;

        public ByteDocumentContent(string content, string? contentType = null)
            : this(Encoding.UTF8.GetBytes(content), contentType, Encoding.UTF8.WebName)
        {
        }

        public ByteDocumentContent(byte[] bytes, string? contentType = null, string? charset = null)
        {
            this.bytes = bytes;
            if (!string.IsNullOrWhiteSpace(charset))
            {
                contentType ??= ContentTypes.PlainText;
                contentType = $"{contentType};charset={charset}";
            }

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