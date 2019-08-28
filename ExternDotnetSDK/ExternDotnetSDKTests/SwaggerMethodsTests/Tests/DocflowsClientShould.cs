using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KeApiOpenSdk.Models.Certificates;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.Docflows;
using KeApiOpenSdk.Models.Documents;
using KeApiOpenSdk.Models.Documents.Data;
using NUnit.Framework;

#pragma warning disable 1998

namespace KeApiOpenSdkTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class DocflowsClientShould : AllTestsShould
    {
        private readonly Guid badId = Guid.NewGuid();
        private DocflowPage page;
        private DocflowPageItem docflow;
        private List<Document> docflowDocuments;
        private Document document;
        private Signature signature;
        private CertificateDto certificate;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            InitializeClient();
            Account = (await Client.Accounts.GetAccountsAsync(0, 1)).Accounts[0];
            page = await Client.Docflows.GetDocflowsAsync(Account.Id);
            docflow = page.DocflowsPageItem[0];
            docflowDocuments = await Client.Docflows.GetDocumentsAsync(Account.Id, docflow.Id);
            document = docflowDocuments[0];
            signature = (await Client.Docflows.GetDocumentSignaturesAsync(Account.Id, docflow.Id, document.Id))[0];
            certificate = (await Client.Accounts.GetAccountCertificatesAsync(Account.Id, 0, 1)).Certificates[0];
        }

        [OneTimeTearDown]
        public override async Task TearDown()
        {
        }

        [Test]
        public void FailToGetDocflows_WithBadParameters()
        {
            var badFilter = new DocflowFilter
            {
                Take = 1,
                InnKpp = "wrong innKpp"
            };
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Docflows.GetDocflowsAsync(Account.Id, badFilter));
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Docflows.GetDocflowsAsync(badId));
        }

        [Test]
        public void FailToGetSingleDocflow_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Docflows.GetDocflowAsync(Account.Id, badId));
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Docflows.GetDocflowAsync(badId, docflow.Id));
        }

        [Test]
        public void GetSingleDocflow_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await Client.Docflows.GetDocflowAsync(Account.Id, docflow.Id));
        }

        [Test]
        public void FailToGetDocflowDocuments_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Docflows.GetDocumentsAsync(Account.Id, badId));
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Docflows.GetDocumentsAsync(badId, docflow.Id));
        }

        [Test]
        public void GetSingleDocument_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Docflows.GetDocumentAsync(Account.Id, docflow.Id, document.Id));
        }

        [Test]
        public void FailToGetSingleDocument_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentAsync(badId, docflow.Id, document.Id));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentAsync(Account.Id, badId, document.Id));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentAsync(Account.Id, docflow.Id, badId));
        }

        [Test]
        public void GetDocumentDescription_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Docflows.GetDocumentDescriptionAsync(Account.Id, docflow.Id, document.Id));
        }

        [Test]
        public void GetDocumentEncryptedContent_WhenItExists()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Docflows.GetEncryptedDocumentContentAsync(Account.Id, docflow.Id, document.Id));
        }

        [Test]
        public void FailToGetNotExistingEncryptedContent()
        {
            var documentId = docflowDocuments[4].Id;
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetEncryptedDocumentContentAsync(Account.Id, docflow.Id, documentId));
        }

        [Test]
        public void GetDocumentDecryptedContent_WhenItExists()
        {
            var documentId = docflowDocuments[4].Id;
            Assert.DoesNotThrowAsync(
                async () => await Client.Docflows.GetDecryptedDocumentContentAsync(Account.Id, docflow.Id, documentId));
        }

        [Test]
        public void FailToGetNotExistingDecryptedContent()
        {
            var documentId = docflowDocuments[2].Id;
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetEncryptedDocumentContentAsync(Account.Id, docflow.Id, documentId));
        }

        [Test]
        public void FailToGetSignatures_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentSignaturesAsync(badId, docflow.Id, document.Id));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentSignaturesAsync(Account.Id, badId, document.Id));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentSignaturesAsync(Account.Id, docflow.Id, badId));
        }

        [Test]
        public void GetSingleSignature_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Docflows.GetSignatureAsync(Account.Id, docflow.Id, document.Id, signature.Id));
        }

        [Test]
        public void FailToGetSingleSignature_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureAsync(badId, docflow.Id, document.Id, signature.Id));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureAsync(Account.Id, badId, document.Id, signature.Id));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureAsync(Account.Id, docflow.Id, badId, signature.Id));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureAsync(Account.Id, docflow.Id, document.Id, badId));
        }

        [Test]
        public void FailToGetSignatureContent_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureContentAsync(badId, docflow.Id, document.Id, signature.Id));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureContentAsync(Account.Id, badId, document.Id, signature.Id));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureContentAsync(Account.Id, docflow.Id, badId, signature.Id));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureContentAsync(Account.Id, docflow.Id, document.Id, badId));
        }

        [Test]
        public void GetSignatureContent_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Docflows.GetSignatureContentAsync(
                    Account.Id,
                    docflow.Id,
                    document.Id,
                    signature.Id));
        }

        [Test]
        public void FailToGetApiTask_WithBadApiTaskId()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetApiTaskAsync(Account.Id, docflow.Id, document.Id, badId));
        }

        [Test]
        public void FailToGetDocumentReply_WithBadReplyId()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentReplyAsync(Account.Id, docflow.Id, document.Id, badId));
        }

        [Test]
        public void FailToPrintDocument_WhenDocumentPrintUnsupported()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.PrintDocumentAsync(Account.Id, docflow.Id, document.Id, new byte[] {0}));
        }

        [Test]
        public void FailToDecryptDocumentContent_WithBadCertificate()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.DecryptDocumentContentAsync(
                    Account.Id,
                    docflow.Id,
                    document.Id,
                    new DecryptDocumentRequestData {CertificateBase64 = "bad cert"}));
        }

        [Test]
        public void FailToGenerateDocumentReply_WithBadParameters()
        {
            //goodUrn itself is actually not valid, but i didn't find a valid one
            var goodUrn = new Urn("urn:document:business-registration-reply-receipt");
            var goodContent = Convert.FromBase64String(certificate.Content);
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GenerateDocumentReplyAsync(
                    Guid.Empty,
                    docflow.Id,
                    document.Id,
                    goodUrn,
                    goodContent));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GenerateDocumentReplyAsync(
                    Account.Id,
                    Guid.Empty,
                    document.Id,
                    goodUrn,
                    goodContent));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GenerateDocumentReplyAsync(
                    Account.Id,
                    docflow.Id,
                    Guid.Empty,
                    goodUrn,
                    goodContent));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GenerateDocumentReplyAsync(
                    Account.Id,
                    docflow.Id,
                    document.Id,
                    new Urn("hello", "world"),
                    goodContent));
            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await Client.Docflows.GenerateDocumentReplyAsync(
                    Account.Id,
                    docflow.Id,
                    document.Id,
                    goodUrn,
                    null));
        }

        [Test]
        public void FailToRecognizeDocument_WithoutRecognitionSupport()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.RecognizeDocumentAsync(Account.Id, docflow.Id, document.Id, new byte[] {1}));
        }

        [Test]
        public void FailToRecognizeDocument_WithBadParameters()
        {
            var content = new byte[] {1};
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.RecognizeDocumentAsync(Guid.Empty, docflow.Id, document.Id, content));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.RecognizeDocumentAsync(Account.Id, Guid.Empty, document.Id, content));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.RecognizeDocumentAsync(Account.Id, docflow.Id, Guid.Empty, content));
            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await Client.Docflows.RecognizeDocumentAsync(Account.Id, docflow.Id, document.Id, null));
        }
    }
}