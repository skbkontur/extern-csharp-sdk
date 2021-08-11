using System;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Models.Drafts;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Http.Constants;
using Kontur.Extern.Client.Model;
using Kontur.Extern.Client.Model.Documents;
using Kontur.Extern.Client.Model.Documents.Contents;
using NSubstitute;
using NUnit.Framework;
using DraftDocument = Kontur.Extern.Client.Model.Drafts.DraftDocument;

namespace Kontur.Extern.Client.Tests.Client.Model.Documents
{
    [TestFixture]
    internal class DraftDocument_Tests
    {
        [Test]
        public void WithId_should_return_a_document_with_given_id()
        {
            var content = CreateContent();
            var id = Guid.NewGuid();

            var document = DraftDocument.WithId(id, content);

            document.DocumentId.Should().Be(id);
        }

        [Test]
        public void WithId_should_fail_when_given_null_document()
        {
            var id = Guid.NewGuid();
            
            Action action = () => DraftDocument.WithId(id, null!);

            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void WithNewId_should_return_a_document_with_given_id()
        {
            var content = CreateContent();

            var document = DraftDocument.WithNewId(content);

            document.DocumentId.Should().NotBeEmpty();
        }

        [Test]
        public void WithNewId_should_fail_when_given_null_document()
        {
            Action action = () => DraftDocument.WithNewId(null!);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void OfType_should_fail_when_given_empty_type()
        {
            var document = DraftDocument.WithNewId(CreateContent());

            Action action = () => document.OfType(default);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void WithSignature_should_fail_when_given_null_signature()
        {
            var document = DraftDocument.WithNewId(CreateContent());

            Action action = () => document.WithSignature(null!);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void WithCertificate_should_fail_when_given_null_certificate()
        {
            var document = DraftDocument.WithNewId(CreateContent());

            Action action = () => document.WithCertificate(null!);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void WithSignature_should_fail_when_given_null_certificate()
        {
            var document = DraftDocument.WithNewId(CreateContent());

            Action action = () => document.WithSignature(null!);

            action.Should().Throw<ArgumentException>();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void WithSvdregCode_should_fail_when_given_invalid_code(string code)
        {
            var document = DraftDocument.WithNewId(CreateContent());

            Action action = () => document.WithSvdregCode(code);

            action.Should().Throw<ArgumentException>();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void WithFileName_should_fail_when_given_invalid_file_name(string fileName)
        {
            var document = DraftDocument.WithNewId(CreateContent());

            Action action = () => document.WithFileName(fileName);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreateSignedRequestAsync_should_fail_when_given_null_crypt()
        {
            var contentId = Guid.NewGuid();
            var document = DraftDocument.WithNewId(CreateContent());

            Func<Task> func = async () => await document.CreateSignedRequestAsync(contentId, null!);

            func.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public async Task CreateSignedRequestAsync_should_return_request_with_content_only()
        {
            var contentId = Guid.NewGuid();
            const string contentType = "application/pdf";
            var expectedRequest = new DocumentRequest
            {
                Description = new DocumentDescriptionRequest {ContentType = contentType},
                ContentId = contentId
            };
            
            var content = CreateContent(contentType);
            var document = DraftDocument.WithNewId(content);

            await CreateSignedRequestAsync_should_return_request(document, contentId, expectedRequest);
        }
        
        [Test]
        public async Task CreateSignedRequestAsync_should_return_request_with_content_only_but_without_content_type()
        {
            var contentId = Guid.NewGuid();
            var expectedRequest = new DocumentRequest
            {
                ContentId = contentId
            };
            
            var document = DraftDocument.WithNewId(CreateContent(null));

            await CreateSignedRequestAsync_should_return_request(document, contentId, expectedRequest);
        }
        
        [Test]
        public async Task CreateSignedRequestAsync_should_return_request_with_SvdregCode()
        {
            var contentId = Guid.NewGuid();
            const string svdregCode = "the code";
            var expectedRequest = new DocumentRequest
            {
                ContentId = contentId,
                Description = new DocumentDescriptionRequest
                {
                    SvdregCode = svdregCode
                }
            };

            var document = DraftDocument.WithNewId(CreateContent(null))
                .WithSvdregCode(svdregCode);

            await CreateSignedRequestAsync_should_return_request(document, contentId, expectedRequest);
        }
        
        [Test]
        public async Task CreateSignedRequestAsync_should_return_request_with_document_type()
        {
            var contentId = Guid.NewGuid();
            var documentType = DocumentType.Fns.Fns534Report.Report;
            var expectedRequest = new DocumentRequest
            {
                ContentId = contentId,
                Description = new DocumentDescriptionRequest
                {
                    Type = documentType.ToUrn()!
                }
            };
            
            var document = DraftDocument.WithNewId(CreateContent(null))
                .OfType(documentType);

            await CreateSignedRequestAsync_should_return_request(document, contentId, expectedRequest);
        }

        [Test]
        public async Task CreateSignedRequestAsync_should_return_request_with_file_name()
        {
            var contentId = Guid.NewGuid();
            const string filename = "report.xml";
            var expectedRequest = new DocumentRequest
            {
                ContentId = contentId,
                Description = new DocumentDescriptionRequest
                {
                    Filename = filename
                }
            };
            
            var document = DraftDocument.WithNewId(CreateContent(null))
                .WithFileName(filename);

            await CreateSignedRequestAsync_should_return_request(document, contentId, expectedRequest);
        }

        private static async Task CreateSignedRequestAsync_should_return_request(DraftDocument document, Guid contentId, DocumentRequest expectedRequest)
        {
            var crypt = Substitute.For<ICrypt>();

            var request = await document.CreateSignedRequestAsync(contentId, crypt);

            request.Should().BeEquivalentTo(expectedRequest);
        }

        [Test]
        public async Task CreateSignedRequestAsync_should_not_sign_content_if_the_cert_or_signature_specified()
        {
            var crypt = Substitute.For<ICrypt>();
            var contentId = Guid.NewGuid();
            var document = DraftDocument.WithNewId(CreateContent());

            var request = await document.CreateSignedRequestAsync(contentId, crypt);

            request.Signature.Should().BeNull();
            crypt.DidNotReceive().Sign(Arg.Any<byte[]>(), Arg.Any<byte[]>());
        }

        [Test]
        public async Task CreateSignedRequestAsync_should_return_signed_content_if_signature_specified()
        {
            var crypt = Substitute.For<ICrypt>();
            var contentId = Guid.NewGuid();
            var expectedSignature = new byte[] {1, 2, 3};
            var documentContent = CreateContent();
            var document = DraftDocument.WithNewId(documentContent)
                .WithSignature(expectedSignature);

            var request = await document.CreateSignedRequestAsync(contentId, crypt);

            request.Signature.Should().BeEquivalentTo(expectedSignature);
            _ = documentContent.DidNotReceive().SignAsync(Arg.Any<CertificateContent>(), Arg.Any<ICrypt>());
        }

        [Test]
        public async Task CreateSignedRequestAsync_should_sign_content_if_a_cert_specified()
        {
            var crypt = Substitute.For<ICrypt>();
            var contentId = Guid.NewGuid();
            var expectedSignature = new byte[] {1, 2, 3};
            var documentContent = CreateContent();
            var document = DraftDocument.WithNewId(documentContent)
                .WithCertificate(new byte[] {1, 2, 3, 4});

            documentContent.SignAsync(Arg.Any<CertificateContent>(), Arg.Any<ICrypt>())
                .Returns(expectedSignature);

            var request = await document.CreateSignedRequestAsync(contentId, crypt);

            request.Signature.Should().BeEquivalentTo(expectedSignature);
        }

        private static IDocumentContent CreateContent(string contentType = ContentTypes.Json)
        {
            var content = Substitute.For<IDocumentContent>();
            content.ContentType.Returns(contentType);
            return content;
        }
    }
}