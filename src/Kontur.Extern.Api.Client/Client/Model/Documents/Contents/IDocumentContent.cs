using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Uploading;
using Kontur.Extern.Api.Client.Cryptography;

namespace Kontur.Extern.Api.Client.Model.Documents.Contents
{
    public interface IDocumentContent
    {
        string? ContentType { get; }

        Task<Signature> SignAsync(CertificateContent certificate, ICrypt crypt);

        Task<Guid> UploadAsync(IContentService contentService, Guid accountId, TimeSpan? chunkUploadTimeout);
    }
}