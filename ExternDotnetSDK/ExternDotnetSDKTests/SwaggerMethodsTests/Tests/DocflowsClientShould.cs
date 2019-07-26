//using System;
//using System.IO;
//using System.Net.Http;
//using System.Threading.Tasks;
//using ExternDotnetSDK.Clients.Account;
//using ExternDotnetSDK.Clients.Docflows;
//using ExternDotnetSDK.Docflows;
//using ExternDotnetSDKTests.SwaggerMethodsTests.Common;
//using Newtonsoft.Json;
//using NUnit.Framework;
//using Refit;

//namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
//{
//    [TestFixture]
//    internal class DocflowsClientShould : AllTestsShould
//    {
//        private const int MaxIndex = 10;

//        private Guid randomDocflowId;
//        private Guid defaultDocflowId;
//        private Guid defaultDocumentId;
//        private DocflowPage defaultDocflowPage;
//        private List<Document> defaultDocflowDocuments;
//        private List<Signature> defaultDocumentSignatures;
//        private readonly Random rnd = new Random();
//        private readonly Guid badId = Guid.NewGuid();
//        private readonly DocflowFilter defaultFilter = new DocflowFilter
//        {
//            Take = MaxIndex
//        };

//        [OneTimeSetUp]
//        public override async Task SetUp()
//        {
//            try
//            {
//                await base.SetUp();
//                defaultAccountId = Account.Id;
//                defaultDocflowPage = await docflowsClient.GetDocflowsAsync(defaultAccountId, defaultFilter);
//                randomDocflowId = defaultDocflowPage.DocflowsPageItem[rnd.Next(MaxIndex)].Id;
//                defaultDocflowId = defaultDocflowPage.DocflowsPageItem[0].Id;
//                defaultDocflowDocuments = await docflowsClient.GetDocumentsAsync(defaultAccountId, defaultDocflowId);
//                defaultDocumentId = defaultDocflowDocuments[0].Id;
//                defaultDocumentSignatures = await docflowsClient.GetDocumentSignaturesAsync(defaultAccountId, defaultDocflowId, defaultDocumentId);
//            }
//            catch (ApiException)
//            {
//                ReadyToTest = false;
//            }
//        }

//        [Test]
//        public void GetDocflows_WithoutParameters() =>
//            Assert.DoesNotThrowAsync(async () => await docflowsClient.GetDocflowsAsync(Account.Id, defaultFilter));

//        [Test]
//        public void FailToGetDocflows_WithNegativeTakeParameter() => Assert.ThrowsAsync<ApiException>(
//            async () => await docflowsClient.GetDocflowsAsync(Account.Id, new DocflowFilter { Take = -1 }));

//        [Test]
//        public void FailToGetDocflows_WithBadInnKpp() => Assert.ThrowsAsync<ApiException>(
//            async () => await docflowsClient.GetDocflowsAsync(Account.Id, new DocflowFilter { Take = 1, InnKpp = "wrong innKpp" }));

//        [Test]
//        public void FailToGetSingleDocflow_WithBadParameters() =>
//            Assert.ThrowsAsync<ApiException>(async () => await docflowsClient.GetDocflowAsync(badId, badId));

//        [Test]
//        public void GetSingleDocflow_WithValidParameters() =>
//            Assert.DoesNotThrowAsync(async () => await docflowsClient.GetDocflowAsync(defaultAccountId, randomDocflowId));

//        [Test]
//        public void GetDocflowDocuments_WithValidParameters() =>
//            Assert.DoesNotThrowAsync(async () => await docflowsClient.GetDocumentsAsync(defaultAccountId, randomDocflowId));

//        [Test]
//        public void FailToGetDocflowDocuments_WithBadParameters()
//        {
//            var goodDocflowId = defaultDocflowPage.DocflowsPageItem[0].Id;
//            Assert.ThrowsAsync<ApiException>(async () => await docflowsClient.GetDocumentsAsync(defaultAccountId, badId));
//            Assert.ThrowsAsync<ApiException>(async () => await docflowsClient.GetDocumentsAsync(badId, goodDocflowId));
//        }

//        [Test]
//        public void GetSingleDocument_WithValidParameters() => Assert.DoesNotThrowAsync(
//            async () => await docflowsClient.GetDocumentAsync(defaultAccountId, defaultDocflowId, defaultDocumentId));

//        [Test]
//        public void GetDocumentDescription_WithValidParameters() => Assert.DoesNotThrowAsync(
//            async () => await docflowsClient.GetDocumentDescriptionAsync(defaultAccountId, defaultDocflowId, defaultDocumentId));

//        [Test]
//        public void GetDocumentEncryptedContent_WhenItExists() => Assert.DoesNotThrowAsync(
//            async () => await docflowsClient.GetEncryptedDocumentContentAsync(defaultAccountId, defaultDocflowId, defaultDocumentId));

//        [Test]
//        public void FailToGetNotExistingEncryptedContent() => Assert.ThrowsAsync<ApiException>(
//            async () => await docflowsClient.GetEncryptedDocumentContentAsync(
//                defaultAccountId,
//                defaultDocflowId,
//                defaultDocflowDocuments[1].Id));

//        [Test]
//        public void GetDocumentDecryptedContent_WhenItExists() => Assert.DoesNotThrowAsync(
//            async () => await docflowsClient.GetDecryptedDocumentContentAsync(
//                defaultAccountId,
//                defaultDocflowId,
//                defaultDocflowDocuments[1].Id));

//        [Test]
//        public void FailToGetNotExistingDecryptedContent() => Assert.ThrowsAsync<ApiException>(
//            async () => await docflowsClient.GetEncryptedDocumentContentAsync(
//                defaultAccountId,
//                defaultDocflowId,
//                defaultDocflowDocuments[2].Id));

//        [Test]
//        public void GetDocumentSignatures_WithValidParameters() => Assert.DoesNotThrowAsync(
//            async () => await docflowsClient.GetDocumentSignaturesAsync(defaultAccountId, defaultDocflowId, defaultDocumentId));

//        [Test]
//        public void GetSignature_WithValidParameters() => Assert.DoesNotThrowAsync(
//            async () => await docflowsClient.GetSignatureAsync(
//                defaultAccountId,
//                defaultDocflowId,
//                defaultDocumentId,
//                defaultDocumentSignatures[0].Id));

//        [Test]
//        public void GetSignatureContent_WithValidParameters() => Assert.DoesNotThrowAsync(
//            async () => await docflowsClient.GetSignatureContentAsync(
//                defaultAccountId,
//                defaultDocflowId,
//                defaultDocumentId,
//                defaultDocumentSignatures[0].Id));
//    }
//}