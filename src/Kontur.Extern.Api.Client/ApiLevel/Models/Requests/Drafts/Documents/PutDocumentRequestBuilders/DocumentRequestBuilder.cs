using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Api.Client.Models.Numbers.BusinessRegistration;
using Kontur.Extern.Api.Client.Uploading;
using Kontur.Extern.Api.Client.Cryptography;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders
{
    internal class DocumentRequestBuilder
    {
        private SvdregCode? svdregCode;
        private string? fileName;
        private DocumentType? type;
        private IDocumentContentUploadStrategy? contentUploadStrategy;
        private bool isDocumentEncrypted;

        public DocumentRequestBuilder SetSvdregCode(SvdregCode code)
        {
            if (code.IsEmpty)
                throw Errors.ValueShouldNotBeEmpty(nameof(code));
            svdregCode = code;
            return this;
        }

        public DocumentRequestBuilder SetFileName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(value));
            fileName = value;
            return this;
        }

        public DocumentRequestBuilder SetType(in DocumentType documentType)
        {
            if (documentType == default)
                throw Errors.ValueShouldNotBeEmpty(nameof(documentType));
            type = documentType;
            return this;
        }

        public DocumentRequestBuilder SetContentUploadStrategy(IDocumentContentUploadStrategy uploadStrategy)
        {
            contentUploadStrategy = uploadStrategy;
            return this;
        }
        
        public DocumentRequestBuilder SetIsDocumentEncrypted(bool isDocumentEncrypted)
        {
            this.isDocumentEncrypted = isDocumentEncrypted;
            return this;
        }

        public async ValueTask<(Signature? signature, DocumentRequest documentRequest)> CreateRequestAsync(
            Guid accountId,
            IContentService uploader,
            ICrypt crypt,
            TimeSpan? uploadTimeout)
        {
            if (contentUploadStrategy == null)
                return (null, ToRequest(null, null));

            var (contentId, signature) = await contentUploadStrategy.UploadAndSignAsync(accountId, uploader, crypt, uploadTimeout).ConfigureAwait(false);
            return (signature, ToRequest(contentId, contentUploadStrategy.ContentType));
        }

        private DocumentRequest ToRequest(Guid? contentId, string? contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
            {
                contentType = null;
            }
            contentType = contentType switch
            {
                null when contentId.HasValue => null,
                not null when contentId is null => null,
                _ => contentType
            };

            var description = new DocumentDescriptionRequest
            {
                IsDocumentEncrypted = isDocumentEncrypted,
                Type = type,
                Filename = fileName,
                SvdregCode = svdregCode,
                ContentType = contentType
            };
            
            return new DocumentRequest
            {
                ContentId = contentId,
                Description = description
            };
        }
    }
}