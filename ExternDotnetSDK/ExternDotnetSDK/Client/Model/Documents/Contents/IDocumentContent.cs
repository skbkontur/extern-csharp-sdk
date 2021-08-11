#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Uploading;

namespace Kontur.Extern.Client.Model.Documents.Contents
{
    public interface IDocumentContent
    {
        string? ContentType { get; }

        Task<Signature> SignAsync(CertificateContent certificate, ICrypt crypt);

        Task<Guid> UploadAsync(IContentService contentService, Guid accountId, TimeSpan? chunkUploadTimeout);
    }
}