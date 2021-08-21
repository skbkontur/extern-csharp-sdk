#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Drafts;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Model.Documents;
using Kontur.Extern.Client.Model.Documents.Contents;

namespace Kontur.Extern.Client.Model.Drafts
{
    public class DraftDocument
    {
        // todo: add method without document
        public static DraftDocument WithNewId(IDocumentContent documentContent) => 
            WithId(Guid.NewGuid(), documentContent);

        public static DraftDocument WithId(Guid documentId, IDocumentContent documentContent) => 
            new(documentId, documentContent ?? throw new ArgumentNullException(nameof(documentContent)));

        private Signature? signature;
        private CertificateContent? certificate;
        private string? svdregCode;
        private string? fileName;
        private DocumentType type;

        private DraftDocument(Guid documentId, IDocumentContent documentContent)
        {
            DocumentId = documentId;
            DocumentContent = documentContent;
        }

        public Guid DocumentId { get; }
        public IDocumentContent DocumentContent { get; }

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

        public DraftDocument WithSvdregCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(code));
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

        internal async Task<DocumentRequest> CreateSignedRequestAsync(Guid contentId, ICrypt crypt)
        {
            if (crypt == null)
                throw new ArgumentNullException(nameof(crypt));

            var signatureOfContent = await SignContentAsync().ConfigureAwait(false);
            return ToRequest(signatureOfContent);
            
            DocumentRequest ToRequest(Signature? overridenSignature)
            {
                var documentTypeUrn = type.ToUrn();
                var contentType = DocumentContent.ContentType;
                DocumentDescriptionRequest? description = null;
                if (svdregCode is not null || fileName is not null || documentTypeUrn is not null || contentType is not null)
                {
                    description = new DocumentDescriptionRequest
                    {
                        Type = documentTypeUrn,
                        Filename = fileName,
                        SvdregCode = svdregCode,
                        ContentType = contentType
                    };
                }
            
                return new DocumentRequest
                {
                    ContentId = contentId,
                    Signature = overridenSignature?.ToBytes(),
                    Description = description
                };
            }

            async Task<Signature?> SignContentAsync()
            {
                return certificate == null 
                    ? signature 
                    : await DocumentContent.SignAsync(certificate, crypt).ConfigureAwait(false);
            }
        }
    }
}