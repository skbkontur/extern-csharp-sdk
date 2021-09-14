#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Model.Documents.Contents;
using Kontur.Extern.Api.Client.Uploading;
using Kontur.Extern.Api.Client.Cryptography;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders
{
    internal class DocumentContentToUpload : IDocumentContentUploadStrategy
    {
        private readonly IDocumentContent documentContent;
        private readonly CertificateContent? certificate;
        private readonly Signature? signature;

        public DocumentContentToUpload(IDocumentContent documentContent, CertificateContent certificate)
            : this(documentContent)
        {
            this.certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
        }

        public DocumentContentToUpload(IDocumentContent documentContent, Signature signature)
            : this(documentContent)
        {
            this.signature = signature ?? throw new ArgumentNullException(nameof(signature));
        }

        public DocumentContentToUpload(IDocumentContent documentContent) =>
            this.documentContent = documentContent ?? throw new ArgumentNullException(nameof(documentContent));

        public string? ContentType => documentContent.ContentType;

        public async ValueTask<(Guid? contentId, Signature? signature)> UploadAndSignAsync(Guid accountId, IContentService uploader, ICrypt crypt, TimeSpan? uploadTimeout)
        {
            if (crypt == null)
                throw new ArgumentNullException(nameof(crypt));
            
            if (uploader == null)
                throw new ArgumentNullException(nameof(uploader));
            
            var contentId = await documentContent.UploadAsync(uploader, accountId, uploadTimeout).ConfigureAwait(false);
            var signatureOfContent = certificate == null 
                ? signature 
                : await documentContent.SignAsync(certificate, crypt).ConfigureAwait(false);
            return (contentId, signatureOfContent);
        }
    }
}