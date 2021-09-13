#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Http.Constants;
using Kontur.Extern.Client.Model;
using Kontur.Extern.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Client.Models.Numbers.BusinessRegistration;
using Kontur.Extern.Client.Uploading;

namespace Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders
{
    internal class DocumentRequestBuilder
    {
        private SvdregCode? svdregCode;
        private string? fileName;
        private DocumentType type;
        private IDocumentContentUploadStrategy? contentUploadStrategy;
        
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
            var documentTypeUrn = type.ToUrn();
            DocumentDescriptionRequest? description = null;
            if (svdregCode is not null || fileName is not null || documentTypeUrn is not null || contentType is not null)
            {
                contentType = (contentType) switch
                {
                    null when contentId.HasValue => ContentTypes.Binary,
                    not null when contentId is null => null,
                    _ => contentType
                };

                description = new DocumentDescriptionRequest
                {
                    Type = documentTypeUrn,
                    Filename = fileName,
                    SvdregCode = svdregCode ?? default,
                    ContentType = contentType
                };
            }

            return new DocumentRequest
            {
                ContentId = contentId,
                Description = description
            };
        }
    }
}