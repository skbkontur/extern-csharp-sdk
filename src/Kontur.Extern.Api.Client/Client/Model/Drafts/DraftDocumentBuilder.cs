using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders;
using Kontur.Extern.Api.Client.Model.Documents.Contents;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Api.Client.Models.Numbers.BusinessRegistration;
using Kontur.Extern.Api.Client.Uploading;
using Kontur.Extern.Api.Client.Cryptography;

namespace Kontur.Extern.Api.Client.Model.Drafts
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public static class DraftDocumentBuilder
    {
        public static IDraftDocument FssSedoProviderSubscriptionSubscribeRequestForRegistrationNumber(Guid documentId) =>
            WithId(documentId)
                .WithoutContent()
                .WithType(DocumentType.FssSedoProviderSubscription.SubscribeRequestForRegistrationNumber)
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
        
        [PublicAPI]
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
            private Signature? contentSignature;
            private CertificateContent? contentCertificate;

            internal SpecifyDocumentSignMethod(Guid documentId, IDocumentContent documentContent)
            {
                this.documentId = documentId;
                this.documentContent = documentContent ?? throw new ArgumentNullException(nameof(documentContent));
                requestBuilder = new DocumentRequestBuilder();
            }

            public SpecifyDocumentSignMethod WithSignature(Signature signature)
            {
                contentSignature = signature ?? throw new ArgumentNullException(nameof(signature));
                return this;
            }

            public SpecifyDocumentSignMethod WithCertificate(CertificateContent certificate)
            {
                contentCertificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
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

            public IDraftDocument ToDocument()
            {
                requestBuilder.SetContentUploadStrategy(CreateUploadStrategy());
                return CreateDraftDocument(documentId, requestBuilder);

                DocumentContentToUpload CreateUploadStrategy()
                {
                    if (contentSignature is not null)
                        return new DocumentContentToUpload(documentContent, contentSignature);

                    if (contentCertificate is not null)
                        return new DocumentContentToUpload(documentContent, contentCertificate);

                    return new DocumentContentToUpload(documentContent);
                }
            }
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