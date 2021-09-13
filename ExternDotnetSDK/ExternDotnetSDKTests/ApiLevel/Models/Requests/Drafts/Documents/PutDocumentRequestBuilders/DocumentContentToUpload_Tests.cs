using System;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Model;
using Kontur.Extern.Client.Model.Documents.Contents;
using Kontur.Extern.Client.Uploading;
using NSubstitute;
using NUnit.Framework;
using Vostok.Commons.Time;

namespace Kontur.Extern.Client.Tests.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders
{
    [TestFixture]
    internal class DocumentContentToUpload_Tests
    {
        private readonly Guid accountId;
        private readonly IContentService uploader;
        private readonly IDocumentContent documentContent;
        private readonly ICrypt crypt;
        private readonly Guid expectedContentId;

        public DocumentContentToUpload_Tests()
        {
            accountId = Guid.NewGuid();
            uploader = Substitute.For<IContentService>();
            
            expectedContentId = Guid.NewGuid();
            documentContent = Substitute.For<IDocumentContent>();
            documentContent.UploadAsync(uploader, accountId, Arg.Any<TimeSpan?>())
                .Returns(expectedContentId);
            
            crypt = Substitute.For<ICrypt>();
        }
        
        [Test]
        public async Task Should_upload_content_with_given_signature()
        {   
            var expectedSignature = Signature.FromBytes(new byte[] {3, 4, 5, 6});
            var uploadStrategy = new DocumentContentToUpload(documentContent, expectedSignature);
            
            var (contentId, signature) = await uploadStrategy.UploadAndSignAsync(accountId, uploader, crypt, 1.Seconds());

            contentId.Should().Be(expectedContentId);
            signature.Should().BeEquivalentTo(expectedSignature);
        }

        [Test]
        public async Task Should_upload_and_sign_content_by_given_certificate()
        {
            var expectedSignature = Signature.FromBytes(new byte[] {3, 4, 5, 6, 7});
            var certificateContent = CertificateContent.FromBytes(new byte[] {1, 2, 3});
            documentContent.SignAsync(certificateContent, crypt)
                .Returns(expectedSignature);
            var uploadStrategy = new DocumentContentToUpload(documentContent, certificateContent);
            
            var (contentId, signature) = await uploadStrategy.UploadAndSignAsync(accountId, uploader, crypt, 1.Seconds());

            contentId.Should().Be(expectedContentId);
            signature.Should().BeEquivalentTo(expectedSignature);
        }

        [Test]
        public async Task Should_upload_without_sign()
        {
            var uploadStrategy = new DocumentContentToUpload(documentContent);
            
            var (contentId, signature) = await uploadStrategy.UploadAndSignAsync(accountId, uploader, crypt, 1.Seconds());

            contentId.Should().Be(expectedContentId);
            signature.Should().BeNull();
        }

        [Test]
        public async Task Should_fail_when_given_null_crypto_provider()
        {
            var uploadStrategy = new DocumentContentToUpload(documentContent);
            
            Func<Task> func = async () => await uploadStrategy.UploadAndSignAsync(accountId, uploader, null!, 1.Seconds());

            await func.Should().ThrowAsync<ArgumentException>();
        }

        [Test]
        public async Task Should_fail_when_given_null_uploader()
        {
            var uploadStrategy = new DocumentContentToUpload(documentContent);
            
            Func<Task> func = async () => await uploadStrategy.UploadAndSignAsync(accountId, null!, crypt, 1.Seconds());

            await func.Should().ThrowAsync<ArgumentException>();
        }

        [Test]
        public void Should_fail_when_given_null_content()
        {
            Action action = () => _ = new DocumentContentToUpload(null!);

            action.Should().Throw<ArgumentException>();
        }
    }
}