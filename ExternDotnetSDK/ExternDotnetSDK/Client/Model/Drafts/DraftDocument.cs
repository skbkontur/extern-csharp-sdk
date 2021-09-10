#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts.Documents;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Http.Constants;
using Kontur.Extern.Client.Model.Documents;
using Kontur.Extern.Client.Model.Documents.Contents;
using Kontur.Extern.Client.Model.Numbers.BusinessRegistration;

namespace Kontur.Extern.Client.Model.Drafts
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftDocument : IDraftDocument
    {
        /// <summary>
        /// Создать документ подписки оператора на страхователя для получения документов из СЭДО (без контента)
        /// </summary>
        /// <param name="documentId">Заранее определенный ID документа</param>
        /// <returns></returns>
        public static IDraftDocument FssSedoProviderSubscriptionSubscribeRequestForRegistrationNumber(Guid documentId)
        {
            return new DraftDocument(documentId, null)
                .OfType(DocumentType.Fss.SedoProviderSubscription.SubscribeRequestForRegistrationNumber);
        }

        public static DraftDocument WithNewId(IDocumentContent documentContent) => 
            WithId(Guid.NewGuid(), documentContent);

        public static DraftDocument WithId(Guid documentId, IDocumentContent documentContent) => 
            new(documentId, documentContent ?? throw new ArgumentNullException(nameof(documentContent)));

        private Signature? signature;
        private CertificateContent? certificate;
        private SvdregCode? svdregCode;
        private string? fileName;
        private DocumentType type;
        private readonly IDocumentContent? DocumentContent;

        private DraftDocument(Guid documentId, IDocumentContent? documentContent)
        {
            DocumentId = documentId;
            DocumentContent = documentContent;
        }

        public Guid DocumentId { get; }

        public DraftDocument OfType(in DocumentType documentType)
        {
            if (documentType == default)
                throw Errors.ValueShouldNotBeEmpty(nameof(documentType));
            type = documentType; 
            return this;
        }

        public DraftDocument WithSignature(Signature contentSignature)
        {
            signature = contentSignature ?? throw new ArgumentNullException(nameof(contentSignature));
            certificate = null;
            return this;
        }
        
        public DraftDocument WithCertificate(CertificateContent certificateToSinContent)
        {
            certificate = certificateToSinContent ?? throw new ArgumentNullException(nameof(certificateToSinContent));
            signature = null;
            return this;
        }

        public DraftDocument WithSvdregCode(SvdregCode code)
        {
            if (code.IsEmpty)
                throw Errors.ValueShouldNotBeEmpty(nameof(code));
            svdregCode = code;
            return this;
        }
        
        public DraftDocument WithFileName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(value));
            fileName = value;
            return this;
        }

        bool IDraftDocument.TryGetDocumentContent(out IDocumentContent content)
        {
            if (DocumentContent is not null)
            {
                content = DocumentContent;
                return true;
            }

            content = default!;
            return false;
        }

        async Task<(Signature? signature, DocumentRequest request)> IDraftDocument.CreateSignedRequestAsync(Guid contentId, ICrypt crypt)
        {
            if (DocumentContent == null)
                throw new InvalidOperationException();
            if (crypt == null)
                throw new ArgumentNullException(nameof(crypt));

            var signatureOfContent = await SignContentAsync().ConfigureAwait(false);
            return (signatureOfContent, ToRequest(contentId));

            async Task<Signature?> SignContentAsync()
            {
                return certificate == null 
                    ? signature 
                    : await DocumentContent.SignAsync(certificate, crypt).ConfigureAwait(false);
            }
        }

        DocumentRequest IDraftDocument.CreateRequestWithoutContentAsync()
        {
            if (DocumentContent is not null)
                throw new InvalidOperationException();
            
            return ToRequest(null);
        }
        
        private DocumentRequest ToRequest(Guid? contentId)
        {
            var documentTypeUrn = type.ToUrn();
            var contentType = DocumentContent?.ContentType;
            DocumentDescriptionRequest? description = null;
            if (svdregCode is not null || fileName is not null || documentTypeUrn is not null || contentType is not null)
            {
                if (contentType is null && DocumentContent is not null)
                {
                    contentType = ContentTypes.Binary;
                }

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