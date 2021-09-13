#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts.Documents;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Model.Documents.Contents;
using Kontur.Extern.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Client.Models.Numbers.BusinessRegistration;
using Kontur.Extern.Client.Uploading;

namespace Kontur.Extern.Client.Model.Drafts
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public static class DraftDocumentBuilder
    {
        public static IDraftDocument FssSedoProviderSubscriptionSubscribeRequestForRegistrationNumber(Guid documentId) =>
            WithId(documentId)
                .WithoutContent()
                .WithType(DocumentType.Fss.SedoProviderSubscription.SubscribeRequestForRegistrationNumber)
                .ToDocument();

        public static SpecifyContent WithNewId() => 
            new(Guid.NewGuid());
        
        public static SpecifyContent WithId(Guid documentId) => 
            new(documentId);

        private static DraftDocumentImplementation CreateDraftDocument(Guid documentId, DocumentRequestBuilder requestBuilder) => 
            new(documentId, requestBuilder);

        public class SpecifyContent
        {
            private readonly Guid documentId;

            internal SpecifyContent(Guid documentId) => this.documentId = documentId;

            public SpecifyDocumentSignMethod WithContentToUpload(IDocumentContent documentContent) => new(
                documentId,
                documentContent ?? throw new ArgumentNullException(nameof(documentContent))
            );

            public Configured WithoutContent() => new(documentId, new DocumentRequestBuilder());

            public Configured WithUploadedContent(Guid contentId, string? contentType = null, Signature? signature = null, string? fileName = null)
            {
                var requestBuilder = new DocumentRequestBuilder()
                    .SetContentUploadStrategy(new AlreadyUploadedContent(contentId, contentType, signature));
                if (fileName is not null)
                    requestBuilder.SetFileName(fileName);
                return new(documentId, requestBuilder);
            }
        }

        private interface IConfiguredDocument<out TResult>
        {
            TResult WithType(in DocumentType documentType);
            TResult WithSvdregCode(SvdregCode code);
            IDraftDocument ToDocument();
        }

        public class SpecifyDocumentSignMethod : IConfiguredDocument<SpecifyDocumentSignMethod>
        {
            private readonly Guid documentId;
            private readonly IDocumentContent documentContent;
            private readonly DocumentRequestBuilder requestBuilder;

            internal SpecifyDocumentSignMethod(Guid documentId, IDocumentContent documentContent)
            {
                this.documentId = documentId;
                this.documentContent = documentContent;
                requestBuilder = new DocumentRequestBuilder();
            }

            public SpecifyDocumentSignMethod WithSignature(Signature signature)
            {
                var uploadStrategy = new DocumentContentToUpload(
                    documentContent,
                    signature ?? throw new ArgumentNullException(nameof(signature))
                );
                requestBuilder.SetContentUploadStrategy(uploadStrategy);
                return this;
            }

            public SpecifyDocumentSignMethod WithCertificate(CertificateContent certificate)
            {
                var uploadStrategy = new DocumentContentToUpload(
                    documentContent,
                    certificate ?? throw new ArgumentNullException(nameof(certificate))
                );
                requestBuilder.SetContentUploadStrategy(uploadStrategy);
                return this;
            }

            public SpecifyDocumentSignMethod WithFileName(string fileName)
            {
                requestBuilder.SetFileName(fileName);
                return this;
            }

            public SpecifyDocumentSignMethod WithType(in DocumentType documentType)
            {
                requestBuilder.SetType(documentType);
                return this;
            }
    
            public SpecifyDocumentSignMethod WithSvdregCode(SvdregCode code)
            {
                requestBuilder.SetSvdregCode(code);
                return this;
            }

            public IDraftDocument ToDocument() => 
                CreateDraftDocument(documentId, requestBuilder);
        }

        public class Configured : IConfiguredDocument<Configured>
        {
            private readonly Guid documentId;
            private readonly DocumentRequestBuilder requestBuilder;

            internal Configured(Guid documentId, DocumentRequestBuilder requestBuilder)
            {
                this.documentId = documentId;
                this.requestBuilder = requestBuilder;
            }

            public Configured WithType(in DocumentType documentType)
            {
                requestBuilder.SetType(documentType);
                return this;
            }
    
            public Configured WithSvdregCode(SvdregCode code)
            {
                requestBuilder.SetSvdregCode(code);
                return this;
            }

            public IDraftDocument ToDocument() => 
                CreateDraftDocument(documentId, requestBuilder);
        }

        private class DraftDocumentImplementation : IDraftDocument
        {
            private readonly DocumentRequestBuilder requestBuilder;

            public DraftDocumentImplementation(Guid documentId, DocumentRequestBuilder requestBuilder)
            {
                this.requestBuilder = requestBuilder;
                DocumentId = documentId;
            }

            public Guid DocumentId { get; }

            public ValueTask<(Signature? signature, DocumentRequest request)> CreateSignedRequestAsync(
                Guid accountId,
                IContentService uploader,
                ICrypt crypt,
                TimeSpan? uploadTimeout)
            {
                return requestBuilder.CreateRequestAsync(accountId, uploader, crypt, uploadTimeout);
            }
        }
    }
}