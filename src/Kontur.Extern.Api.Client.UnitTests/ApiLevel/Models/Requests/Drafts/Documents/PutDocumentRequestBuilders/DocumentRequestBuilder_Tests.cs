using System;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders;
using Kontur.Extern.Api.Client.Cryptography;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Api.Client.Models.Numbers.BusinessRegistration;
using Kontur.Extern.Api.Client.Uploading;
using NSubstitute;
using NUnit.Framework;
using Vostok.Commons.Time;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders
{
    [TestFixture]
    internal class DocumentRequestBuilder_Tests
    {
        [Test]
        public void SetType_should_fail_when_given_empty_type()
        {
            var requestBuilder = new DocumentRequestBuilder();

            Action action = () => requestBuilder.SetType(default);

            action.Should().Throw<ArgumentException>();
        }
        
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SetFileName_should_fail_when_invalid_file_name(string fileName)
        {
            var requestBuilder = new DocumentRequestBuilder();

            Action action = () => requestBuilder.SetFileName(fileName);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetSvdregCode_should_fail_when_gi()
        {
            var requestBuilder = new DocumentRequestBuilder();

            Action action = () => requestBuilder.SetSvdregCode(default);

            action.Should().Throw<ArgumentException>();
        }

        [TestCase(null, "application/octet-stream")]
        [TestCase("", "application/octet-stream")]
        [TestCase(" ", "application/octet-stream")]
        [TestCase("application/pdf", "application/pdf")]
        public async Task Should_build_request_with_uploaded_content(string contentType, string expectedContentType)
        {
            var accountId = Guid.NewGuid();
            
            var uploadedContentId = Guid.NewGuid();
            var uploadedSignature = Signature.FromBytes(new byte[] {1, 2, 3});

            const string fileName = "file_name";
            var svdregCode = SvdregCode.ForIndividualEntrepreneur.Code_011011;
            var expectedRequest = new DocumentRequest
            {
                ContentId = uploadedContentId,
                Description = new DocumentDescriptionRequest
                {
                    Filename = fileName,
                    SvdregCode = svdregCode,
                    ContentType = expectedContentType,
                    Type = DocumentType.Fss.Report.ReportFile
                }
            };
            
            var contentService = Substitute.For<IContentService>();
            var crypt = Substitute.For<ICrypt>();
            
            var uploadStrategy = Substitute.For<IDocumentContentUploadStrategy>();
            uploadStrategy.ContentType.Returns(contentType);
            uploadStrategy
                .UploadAndSignAsync(accountId, contentService, crypt, Arg.Any<TimeSpan>())
                .Returns((uploadedContentId, uploadedSignature));
            
            var (signature, documentRequest) = await new DocumentRequestBuilder()
                .SetType(DocumentType.Fss.Report.ReportFile)
                .SetFileName(fileName)
                .SetSvdregCode(svdregCode)
                .SetContentUploadStrategy(uploadStrategy)
                .CreateRequestAsync(accountId, contentService, crypt, 1.Seconds());

            signature.Should().BeEquivalentTo(uploadedSignature);
            documentRequest.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public async Task Should_build_request_without_content_if_upload_strategy_do_not_upload()
        {
            var accountId = Guid.NewGuid();

            const string fileName = "file_name";
            var svdregCode = SvdregCode.ForIndividualEntrepreneur.Code_011011;
            var expectedRequest = new DocumentRequest
            {
                Description = new DocumentDescriptionRequest
                {
                    Filename = fileName,
                    SvdregCode = svdregCode,
                    Type = DocumentType.Fss.Report.ReportFile
                }
            };
            
            var contentService = Substitute.For<IContentService>();
            var crypt = Substitute.For<ICrypt>();
            
            var uploadStrategy = Substitute.For<IDocumentContentUploadStrategy>();
            uploadStrategy.ContentType.Returns("application/pdf");
            uploadStrategy
                .UploadAndSignAsync(accountId, contentService, crypt, Arg.Any<TimeSpan>())
                .Returns((null, null));
            
            var (signature, documentRequest) = await new DocumentRequestBuilder()
                .SetType(DocumentType.Fss.Report.ReportFile)
                .SetFileName(fileName)
                .SetSvdregCode(svdregCode)
                .SetContentUploadStrategy(uploadStrategy)
                .CreateRequestAsync(accountId, contentService, crypt, 1.Seconds());

            signature.Should().BeNull();
            documentRequest.Should().BeEquivalentTo(expectedRequest);
        }

        [Test]
        public async Task Should_build_request_without_content()
        {
            var accountId = Guid.NewGuid();
            
            const string fileName = "file_name";
            var svdregCode = SvdregCode.ForIndividualEntrepreneur.Code_011011;
            var expectedRequest = new DocumentRequest
            {
                ContentId = null,
                Description = new DocumentDescriptionRequest
                {
                    Filename = fileName,
                    SvdregCode = svdregCode,
                    Type = DocumentType.Fss.Report.ReportFile
                }
            };
            
            var contentService = Substitute.For<IContentService>();
            var crypt = Substitute.For<ICrypt>();
            
            var (signature, documentRequest) = await new DocumentRequestBuilder()
                .SetType(DocumentType.Fss.Report.ReportFile)
                .SetFileName(fileName)
                .SetSvdregCode(svdregCode)
                .CreateRequestAsync(accountId, contentService, crypt, 1.Seconds());

            signature.Should().BeNull();
            documentRequest.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public async Task Should_build_request_without_content_and_metadata()
        {
            var accountId = Guid.NewGuid();
            
            var expectedRequest = new DocumentRequest();
            
            var contentService = Substitute.For<IContentService>();
            var crypt = Substitute.For<ICrypt>();
            
            var (signature, documentRequest) = await new DocumentRequestBuilder()
                .CreateRequestAsync(accountId, contentService, crypt, 1.Seconds());

            signature.Should().BeNull();
            documentRequest.Should().BeEquivalentTo(expectedRequest);
        }
    }
}