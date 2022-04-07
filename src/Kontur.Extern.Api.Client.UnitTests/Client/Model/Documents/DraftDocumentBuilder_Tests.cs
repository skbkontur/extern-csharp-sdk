using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents;
using Kontur.Extern.Api.Client.Cryptography;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Model.Documents.Contents;
using Kontur.Extern.Api.Client.Model.Drafts;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Api.Client.Models.Numbers.BusinessRegistration;
using Kontur.Extern.Api.Client.Uploading;
using NSubstitute;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.Documents
{
    [TestFixture]
    internal class DraftDocumentBuilder_Tests
    {
        [Test]
        public void WithNewId_should_specify_new_document_id()
        {
            var document = DraftDocumentBuilder
                .WithNewId()
                .WithoutContent()
                .ToDocument();

            document.DocumentId.Should().NotBeEmpty();
        }
        
        [Test]
        public void WithId_should_specify_given_document_id()
        {
            var documentId = Guid.NewGuid();
            var document = DraftDocumentBuilder
                .WithId(documentId)
                .WithoutContent()
                .ToDocument();

            document.DocumentId.Should().Be(documentId);

        }
        
        [Test]
        public async Task ToDocument_should_create_document_with_signed_content_to_upload()
        {
            var accountId = Guid.NewGuid();
            
            var uploadedContentId = Guid.NewGuid();
            var uploadedSignature = Signature.FromBytes(new byte[] {1, 2, 3});

            const string fileName = "file_name";
            var svdregCode = SvdregCode.ForIndividualEntrepreneur.Code_011011;
            const string contentType = "application/pdf";
            var documentType = DocumentType.Fss.FssReport.Report;
            var expectedRequest = new DocumentRequest
            {
                ContentId = uploadedContentId,
                Description = new DocumentDescriptionRequest
                {
                    Filename = fileName,
                    SvdregCode = svdregCode,
                    ContentType = contentType,
                    Type = documentType
                }
            };
            
            var contentService = Substitute.For<IContentService>();
            var crypt = Substitute.For<ICrypt>();

            var documentContent = Substitute.For<IDocumentContent>();
            documentContent.ContentType.Returns(contentType);
            documentContent.SignAsync(Arg.Any<CertificateContent>(), crypt).Returns(uploadedSignature);
            documentContent.UploadAsync(contentService, accountId, Arg.Any<TimeSpan?>()).Returns(uploadedContentId);
            
            var document = DraftDocumentBuilder
                .WithNewId()
                .WithContentToUpload(documentContent)
                .WithSignature(uploadedSignature)
                .WithFileName(fileName)
                .WithSvdregCode(svdregCode)
                .WithType(documentType)
                .ToDocument();
            
            var (signature, documentRequest) = await document
                .CreateSignedRequestAsync(accountId, contentService, crypt, 1.Seconds());

            signature.Should().BeEquivalentTo(uploadedSignature);
            documentRequest.Should().BeEquivalentTo(expectedRequest);
        }

        [Test]
        public async Task ToDocument_should_create_document_with_not_signed_content_to_upload()
        {
            var accountId = Guid.NewGuid();
            
            var uploadedContentId = Guid.NewGuid();
            const string contentType = "application/octet-stream";
            var documentType = DocumentType.Fns.Fns534Report.Report;
            var expectedRequest = new DocumentRequest
            {
                ContentId = uploadedContentId,
                Description = new DocumentDescriptionRequest
                {
                    ContentType = contentType,
                    Type = documentType
                }
            };
            
            var contentService = Substitute.For<IContentService>();
            var crypt = Substitute.For<ICrypt>();

            var documentContent = Substitute.For<IDocumentContent>();
            documentContent.UploadAsync(contentService, accountId, Arg.Any<TimeSpan?>()).Returns(uploadedContentId);
            
            var document = DraftDocumentBuilder
                .WithNewId()
                .WithContentToUpload(documentContent)
                .WithType(documentType)
                .ToDocument();
            
            var (signature, documentRequest) = await document
                .CreateSignedRequestAsync(accountId, contentService, crypt, 1.Seconds());

            signature.Should().BeNull();
            documentRequest.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public async Task FssSedoProviderSubscriptionSubscribeRequestForRegistrationNumber_should_create_document_without_content()
        {
            var accountId = Guid.NewGuid();
            var expectedRequest = new DocumentRequest
            {
                Description = new DocumentDescriptionRequest
                {
                    Type = DocumentType.Fss.SedoProviderSubscription.SubscribeRequestForRegistrationNumber
                }
            };
            
            var contentService = Substitute.For<IContentService>();
            var crypt = Substitute.For<ICrypt>();
            var documentId = Guid.NewGuid();
            
            var document = DraftDocumentBuilder
                .FssSedoProviderSubscriptionSubscribeRequestForRegistrationNumber(documentId);

            document.DocumentId.Should().Be(documentId);
            
            var (signature, documentRequest) = await document
                .CreateSignedRequestAsync(accountId, contentService, crypt, 1.Seconds());

            signature.Should().BeNull();
            documentRequest.Should().BeEquivalentTo(expectedRequest);
        }
    }
}