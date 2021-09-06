using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Responses.Docflows;
using Kontur.Extern.Client.Models.Certificates;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Docflows.Documents;
using NUnit.Framework;

#pragma warning disable 1998

namespace Kontur.Extern.Client.Tests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class DocflowsClientShould : AllTestsShould
    {
        private readonly Guid badId = Guid.NewGuid();
        private DocflowPage page;
        private IDocflow docflow;
        private List<Document> docflowDocuments;
        private Document document;
        private Signature signature;
        private Certificate certificate;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            InitializeClient();
            Account = (await Client.Accounts.GetAccountsAsync(0, 1).ConfigureAwait(false)).Accounts[0];
            page = await Client.Docflows.GetDocflowsAsync(Account.Id).ConfigureAwait(false);
            docflow = page.DocflowsPageItem[0];
            docflowDocuments = await Client.Docflows.GetDocumentsAsync(Account.Id, docflow.Id).ConfigureAwait(false);
            document = docflowDocuments[0];
            signature = (await Client.Docflows.GetDocumentSignaturesAsync(Account.Id, docflow.Id, document.Id).ConfigureAwait(false))[0];
            certificate = (await Client.Accounts.GetAccountCertificatesAsync(Account.Id, 0, 1).ConfigureAwait(false)).Certificates[0];
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
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Docflows.GetDocflowsAsync(Account.Id, badFilter).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Docflows.GetDocflowsAsync(badId).ConfigureAwait(false));
        }

        [Test]
        public void FailToGetSingleDocflow_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Docflows.GetDocflowAsync(Account.Id, badId).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Docflows.GetDocflowAsync(badId, docflow.Id).ConfigureAwait(false));
        }

        [Test]
        public void GetSingleDocflow_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await Client.Docflows.GetDocflowAsync(Account.Id, docflow.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToGetDocflowDocuments_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Docflows.GetDocumentsAsync(Account.Id, badId).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Docflows.GetDocumentsAsync(badId, docflow.Id).ConfigureAwait(false));
        }

        [Test]
        public void GetSingleDocument_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Docflows.GetDocumentAsync(Account.Id, docflow.Id, document.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToGetSingleDocument_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentAsync(badId, docflow.Id, document.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentAsync(Account.Id, badId, document.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentAsync(Account.Id, docflow.Id, badId).ConfigureAwait(false));
        }

        [Test]
        public void GetDocumentDescription_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Docflows.GetDocumentDescriptionAsync(Account.Id, docflow.Id, document.Id).ConfigureAwait(false));
        }

        // [Test]
        // public void GetDocumentEncryptedContent_WhenItExists()
        // {
        //     Assert.DoesNotThrowAsync(
        //         async () => await Client.Docflows.GetEncryptedDocumentContentAsync(Account.Id, docflow.Id, document.Id).ConfigureAwait(false));
        // }
        //
        // [Test]
        // public void FailToGetNotExistingEncryptedContent()
        // {
        //     var documentId = docflowDocuments[4].Id;
        //     Assert.ThrowsAsync<HttpRequestException>(
        //         async () => await Client.Docflows.GetEncryptedDocumentContentAsync(Account.Id, docflow.Id, documentId).ConfigureAwait(false));
        // }
        //
        // [Test]
        // public void GetDocumentDecryptedContent_WhenItExists()
        // {
        //     var documentId = docflowDocuments[4].Id;
        //     Assert.DoesNotThrowAsync(
        //         async () => await Client.Docflows.GetDecryptedDocumentContentAsync(Account.Id, docflow.Id, documentId).ConfigureAwait(false));
        // }
        //
        // [Test]
        // public void FailToGetNotExistingDecryptedContent()
        // {
        //     var documentId = docflowDocuments[2].Id;
        //     Assert.ThrowsAsync<HttpRequestException>(
        //         async () => await Client.Docflows.GetEncryptedDocumentContentAsync(Account.Id, docflow.Id, documentId).ConfigureAwait(false));
        // }

        [Test]
        public void FailToGetSignatures_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentSignaturesAsync(badId, docflow.Id, document.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentSignaturesAsync(Account.Id, badId, document.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetDocumentSignaturesAsync(Account.Id, docflow.Id, badId).ConfigureAwait(false));
        }

        [Test]
        public void GetSingleSignature_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Docflows.GetSignatureAsync(Account.Id, docflow.Id, document.Id, signature.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToGetSingleSignature_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureAsync(badId, docflow.Id, document.Id, signature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureAsync(Account.Id, badId, document.Id, signature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureAsync(Account.Id, docflow.Id, badId, signature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureAsync(Account.Id, docflow.Id, document.Id, badId).ConfigureAwait(false));
        }

        [Test]
        public void FailToGetSignatureContent_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureContentAsync(badId, docflow.Id, document.Id, signature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureContentAsync(Account.Id, badId, document.Id, signature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureContentAsync(Account.Id, docflow.Id, badId, signature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetSignatureContentAsync(Account.Id, docflow.Id, document.Id, badId).ConfigureAwait(false));
        }

        [Test]
        public void GetSignatureContent_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Docflows.GetSignatureContentAsync(
                    Account.Id,
                    docflow.Id,
                    document.Id,
                    signature.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToGetApiTask_WithBadApiTaskId()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Docflows.GetPrintDocumentTaskAsync(Account.Id, docflow.Id, document.Id, badId).ConfigureAwait(false));
        }

        [Test]
        public void FailToGetDocumentReply_WithBadReplyId()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Replies.GetReplyAsync(Account.Id, docflow.Id, document.Id, badId).ConfigureAwait(false));
        }

        // [Test]
        // public void FailToPrintDocument_WhenDocumentPrintUnsupported()
        // {
        //     Assert.ThrowsAsync<HttpRequestException>(
        //         async () => await Client.Docflows.PrintDocumentAsync(Account.Id, docflow.Id, document.Id, new byte[] {0}).ConfigureAwait(false));
        // }

        // [Test]
        // public void FailToDecryptDocumentContent_WithBadCertificate()
        // {
        //     Assert.ThrowsAsync<HttpRequestException>(
        //         () => Client.Docflows.StartCloudDecryptDocumentAsync(
        //             Account.Id,
        //             docflow.Id,
        //             document.Id,
        //             new byte[] {1, 2, 3}));
        // }

        [Test]
        public void FailToGenerateDocumentReply_WithBadParameters()
        {
            //goodUrn itself is actually not valid, but i didn't find a valid one
            var goodUrn = Urn.Parse("urn:document:business-registration-reply-receipt");
            var goodContent = certificate.Content;
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Replies.GenerateReplyAsync(
                    Guid.Empty,
                    docflow.Id,
                    document.Id,
                    goodUrn,
                    goodContent).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Replies.GenerateReplyAsync(
                    Account.Id,
                    Guid.Empty,
                    document.Id,
                    goodUrn,
                    goodContent).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Replies.GenerateReplyAsync(
                    Account.Id,
                    docflow.Id,
                    Guid.Empty,
                    goodUrn,
                    goodContent).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Replies.GenerateReplyAsync(
                    Account.Id,
                    docflow.Id,
                    document.Id,
                    new Urn("hello", "world"),
                    goodContent).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Replies.GenerateReplyAsync(
                    Account.Id,
                    docflow.Id,
                    document.Id,
                    goodUrn,
                    null).ConfigureAwait(false));
        }

        // [Test]
        // public void FailToRecognizeDocument_WithoutRecognitionSupport()
        // {
        //     Assert.ThrowsAsync<HttpRequestException>(
        //         async () => await Client.Docflows.RecognizeDocumentAsync(Account.Id, docflow.Id, document.Id, new byte[] {1}).ConfigureAwait(false));
        // }

        // [Test]
        // public void FailToRecognizeDocument_WithBadParameters()
        // {
        //     var content = new byte[] {1};
        //     Assert.ThrowsAsync<HttpRequestException>(
        //         async () => await Client.Docflows.RecognizeDocumentAsync(Guid.Empty, docflow.Id, document.Id, content).ConfigureAwait(false));
        //     Assert.ThrowsAsync<HttpRequestException>(
        //         async () => await Client.Docflows.RecognizeDocumentAsync(Account.Id, Guid.Empty, document.Id, content).ConfigureAwait(false));
        //     Assert.ThrowsAsync<HttpRequestException>(
        //         async () => await Client.Docflows.RecognizeDocumentAsync(Account.Id, docflow.Id, Guid.Empty, content).ConfigureAwait(false));
        //     Assert.ThrowsAsync<ArgumentNullException>(
        //         async () => await Client.Docflows.RecognizeDocumentAsync(Account.Id, docflow.Id, document.Id, null).ConfigureAwait(false));
        // }
    }
}