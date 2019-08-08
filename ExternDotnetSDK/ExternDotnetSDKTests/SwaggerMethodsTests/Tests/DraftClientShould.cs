using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Certificates;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Drafts;
using ExternDotnetSDK.Models.Drafts.Requests;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class DraftClientShould : AllTestsShould
    {
        private DraftMetaRequest validDraftMetaRequest;
        private Draft draft;
        private DraftDocument emptyDocument;
        private DraftDocument filledDocument;
        private Signature filledDocumentSignature;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            InitializeClient();
            Account = (await Client.Accounts.GetAccountsAsync(0, 1)).Accounts[0];
            var cert = (await Client.Certificates.GetCertificatesAsync(Account.Id)).Certificates[0];
            validDraftMetaRequest = new DraftMetaRequest
            {
                Payer = new AccountInfoRequest
                {
                    Inn = cert.Inn,
                    Organization = new OrganizationInfoRequest {Kpp = cert.Kpp}
                },
                Sender = new SenderRequest
                {
                    Inn = cert.Inn,
                    Kpp = cert.Kpp,
                    IpAddress = "8.8.8.8",
                    Certificate = new CertificateRequest {Content = cert.Content}
                },
                Recipient = new RecipientInfoRequest {FssCode = "11111"}
            };
            draft = await CreateDraftAsync();
            emptyDocument = await CreateEmptyDocument();
            filledDocument = await CreateFilledDocument(draft);
            filledDocumentSignature = await Client.Drafts.AddDocumentSignatureAsync(
                Account.Id,
                draft.Id,
                filledDocument.Id,
                new SignatureRequest {Base64Content = Convert.ToBase64String(new byte[] {1, 2, 3, 4})});
        }

        [OneTimeTearDown]
        public override async Task TearDown()
        {
            await Client.Drafts.DeleteDocumentSignatureAsync(
                Account.Id,
                draft.Id,
                filledDocument.Id,
                filledDocumentSignature.Id);
            await Client.Drafts.DeleteDocumentAsync(Account.Id, draft.Id, emptyDocument.Id);
            await Client.Drafts.DeleteDocumentAsync(Account.Id, draft.Id, filledDocument.Id);
            await Client.Drafts.DeleteDraftAsync(Account.Id, draft.Id);
        }

        [Test]
        public void FailToCreateDraft_WithMissingDraftMetaRequest()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDraftAsync(Account.Id, null));
        }

        [Test]
        public void FailToCreateDraft_WithMissingPayer()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDraftAsync(Account.Id, new DraftMetaRequest()));
        }

        [Test]
        public void FailToCreateDraft_WithMissingSender()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest {Payer = new AccountInfoRequest()}));
        }

        [Test]
        public void FailToCreateDraft_WithMissingRecipient()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest
                    {
                        Payer = new AccountInfoRequest(),
                        Sender = new SenderRequest()
                    }));
        }

        [Test]
        public void FailToCreateDraft_WithMissingInnKpp()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest
                    {
                        Payer = new AccountInfoRequest(),
                        Sender = new SenderRequest(),
                        Recipient = new RecipientInfoRequest {FssCode = "11111"}
                    }));
        }

        [Test]
        public void FailToCreateDraft_WithMissingIp()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest
                    {
                        Payer = validDraftMetaRequest.Payer,
                        Sender = new SenderRequest
                        {
                            Inn = validDraftMetaRequest.Sender.Inn,
                            Kpp = validDraftMetaRequest.Sender.Kpp
                        },
                        Recipient = validDraftMetaRequest.Recipient
                    }));
        }

        [Test]
        public void FailToCreateDraft_WithMissingSenderCertificate()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest
                    {
                        Payer = validDraftMetaRequest.Payer,
                        Sender = new SenderRequest
                        {
                            Inn = validDraftMetaRequest.Sender.Inn,
                            IpAddress = "8.8.8.8",
                            Kpp = validDraftMetaRequest.Sender.Kpp
                        },
                        Recipient = validDraftMetaRequest.Recipient
                    }));
        }

        [Test]
        public void FailToCreateDraft_WithEmptySenderCertificate()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest
                    {
                        Payer = validDraftMetaRequest.Payer,
                        Sender = new SenderRequest
                        {
                            Inn = validDraftMetaRequest.Sender.Inn,
                            IpAddress = "8.8.8.8",
                            Kpp = validDraftMetaRequest.Sender.Kpp,
                            Certificate = new CertificateRequest()
                        },
                        Recipient = validDraftMetaRequest.Recipient
                    }));
        }

        [Test]
        public void FailToCreateDraft_WithInconvertableSenderCertificate()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest
                    {
                        Payer = validDraftMetaRequest.Payer,
                        Sender = new SenderRequest
                        {
                            Inn = validDraftMetaRequest.Sender.Inn,
                            IpAddress = "8.8.8.8",
                            Kpp = validDraftMetaRequest.Sender.Kpp,
                            Certificate = new CertificateRequest {Content = "1111"}
                        },
                        Recipient = validDraftMetaRequest.Recipient
                    }));
        }

        [Test]
        public void FailToCreateDraft_WithSenderCertificateOfWrongOrganization()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest
                    {
                        Payer = validDraftMetaRequest.Payer,
                        Sender = new SenderRequest
                        {
                            Inn = "5834043089",
                            IpAddress = "8.8.8.8",
                            Kpp = "036550424",
                            Certificate = validDraftMetaRequest.Sender.Certificate
                        },
                        Recipient = validDraftMetaRequest.Recipient
                    }));
        }

        [Test]
        public void FailToCreateDraft_WithMissingSenderOrganization()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest
                    {
                        Payer = validDraftMetaRequest.Payer,
                        Sender = new SenderRequest
                        {
                            Inn = "5834043089",
                            IpAddress = "8.8.8.8",
                            Kpp = validDraftMetaRequest.Sender.Kpp,
                            Certificate = validDraftMetaRequest.Sender.Certificate
                        },
                        Recipient = validDraftMetaRequest.Recipient
                    }));
        }

        [Test]
        public void FailToDeleteDraft_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.DeleteDraftAsync(Account.Id, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.DeleteDraftAsync(Guid.Empty, draft.Id));
        }

        [Test]
        public void FailToGetDraft_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDraftAsync(Account.Id, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDraftAsync(Guid.Empty, draft.Id));
        }

        [Test]
        public void GetDraft_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.GetDraftAsync(Account.Id, draft.Id));
        }

        [Test]
        public void GetDraftMeta_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.GetDraftMetaAsync(Account.Id, draft.Id));
        }

        [Test]
        public void FailToGetDraftMeta_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDraftMetaAsync(Account.Id, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDraftMetaAsync(Guid.Empty, draft.Id));
        }

        [Test]
        public void UpdateDraftMeta_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.UpdateDraftMetaAsync(Account.Id, draft.Id, validDraftMetaRequest));
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.UpdateDraftMetaAsync(Account.Id, draft.Id, null));
        }

        [Test]
        public async Task ReturnDifferentDraftMeta_WhenUpdatedWithValidAndDifferentParameters()
        {
            var oldMeta = await Client.Drafts.UpdateDraftMetaAsync(Account.Id, draft.Id, validDraftMetaRequest);
            var newMeta = await Client.Drafts.UpdateDraftMetaAsync(
                Account.Id,
                draft.Id,
                new DraftMetaRequest
                {
                    Payer = validDraftMetaRequest.Payer,
                    Sender = validDraftMetaRequest.Sender,
                    Recipient = new RecipientInfoRequest {FssCode = "22222"}
                });
            oldMeta.Should().NotBeEquivalentTo(newMeta);
        }

        [Test]
        public async Task ReturnSameMeta_WhenUpdatedWithInvalidParameters()
        {
            var oldMeta = await Client.Drafts.UpdateDraftMetaAsync(Account.Id, draft.Id, validDraftMetaRequest);
            var newMeta = await Client.Drafts.UpdateDraftMetaAsync(Account.Id, draft.Id, null);
            oldMeta.Should().BeEquivalentTo(newMeta);
        }

        [Test]
        public async Task DeleteDocument_WithValidParameters()
        {
            var document = await CreateEmptyDocument();
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.DeleteDocumentAsync(Account.Id, draft.Id, document.Id));
        }

        [Test]
        public void FailToDeleteDocument_WithInvalidParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.DeleteDocumentAsync(Guid.Empty, draft.Id, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.DeleteDocumentAsync(Account.Id, Guid.Empty, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.DeleteDocumentAsync(Account.Id, draft.Id, Guid.Empty));
        }

        [Test]
        public void FailToCreateDocument_WithWrongBase64Content()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.AddDocumentAsync(
                    Account.Id,
                    draft.Id,
                    new DocumentContents {Base64Content = "1"}));
        }

        [Test]
        public void FailToCreateDocument_WithWrongSignature()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.AddDocumentAsync(
                    Account.Id,
                    draft.Id,
                    new DocumentContents {Signature = "1"}));
        }

        [Test]
        public void GetDocument_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.GetDocumentAsync(Account.Id, draft.Id, emptyDocument.Id));
        }

        [Test]
        public void FailToGetDocument_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentAsync(Guid.Empty, draft.Id, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentAsync(Account.Id, Guid.Empty, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentAsync(Account.Id, draft.Id, Guid.Empty));
        }

        [Test]
        public void FailToUpdateDocument_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentAsync(
                    Account.Id,
                    draft.Id,
                    emptyDocument.Id,
                    new DocumentContents {Base64Content = "1"}));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentAsync(
                    Account.Id,
                    draft.Id,
                    emptyDocument.Id,
                    new DocumentContents {Signature = "1"}));
        }

        [Test]
        public async Task UpdateDocument_WithValidParameters()
        {
            var document = await CreateEmptyDocument();
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.UpdateDocumentAsync(
                    Account.Id,
                    draft.Id,
                    document.Id,
                    new DocumentContents {Base64Content = Convert.ToBase64String(new byte[] {1})}));
            await Client.Drafts.DeleteDocumentAsync(Account.Id, draft.Id, document.Id);
        }

        [Test]
        public void FailToGetDocumentPrint_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentPrintAsync(Guid.Empty, draft.Id, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentPrintAsync(Account.Id, Guid.Empty, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentPrintAsync(Account.Id, draft.Id, Guid.Empty));
        }

        [Test]
        public void FailToGetNonexistentDocumentPrint()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentPrintAsync(Account.Id, draft.Id, emptyDocument.Id));
        }

        [Test]
        public void FailToGetDocumentDecryptedContent_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentDecryptedContentAsync(Guid.Empty, draft.Id, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentDecryptedContentAsync(Account.Id, Guid.Empty, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentDecryptedContentAsync(Account.Id, draft.Id, Guid.Empty));
        }

        [Test]
        public void FailToGetDocumentDecryptedContent_WhenItIsEmpty()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentDecryptedContentAsync(Account.Id, draft.Id, emptyDocument.Id));
        }

        [Test]
        public void GetDocumentDecryptedContent_WhenItExists()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.GetDocumentDecryptedContentAsync(Account.Id, draft.Id, filledDocument.Id));
        }

        [Test]
        public void FailToUpdateDocumentDecryptedContent_WithBadParameters()
        {
            var content = new byte[] {9, 1, 1};
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentDecryptedContentAsync(
                    Guid.Empty,
                    draft.Id,
                    emptyDocument.Id,
                    content));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentDecryptedContentAsync(
                    Account.Id,
                    Guid.Empty,
                    emptyDocument.Id,
                    content));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentDecryptedContentAsync(
                    Account.Id,
                    draft.Id,
                    Guid.Empty,
                    content));
            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await Client.Drafts.UpdateDocumentDecryptedContentAsync(
                    Account.Id,
                    draft.Id,
                    emptyDocument.Id,
                    null));
        }

        [Test]
        public async Task UpdateDocumentDecryptedContent_WithValidParameters()
        {
            var content = new byte[] {1};
            var document = await CreateEmptyDocument();
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.UpdateDocumentDecryptedContentAsync(
                    Account.Id,
                    draft.Id,
                    document.Id,
                    content));
            await Client.Drafts.DeleteDocumentAsync(Account.Id, draft.Id, document.Id);
        }

        [Test]
        public void FailToGetDocumentSignatureContent_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentSignatureContentAsync(Guid.Empty, draft.Id, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentSignatureContentAsync(Account.Id, Guid.Empty, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentSignatureContentAsync(Account.Id, draft.Id, Guid.Empty));
        }

        [Test]
        public void FailToGetDocumentSignatureContent_WhenItIsEmpty()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentSignatureContentAsync(Account.Id, draft.Id, emptyDocument.Id));
        }

        [Test]
        public void GetDocumentSignatureContent_WhenItExists()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.GetDocumentSignatureContentAsync(Account.Id, draft.Id, filledDocument.Id));
        }

        [Test]
        public void FailToUpdateSignatureContent_WithBadParameters()
        {
            var content = new byte[] {1};
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentSignatureContentAsync(
                    Guid.Empty,
                    draft.Id,
                    emptyDocument.Id,
                    content));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentSignatureContentAsync(
                    Account.Id,
                    Guid.Empty,
                    emptyDocument.Id,
                    content));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentSignatureContentAsync(
                    Account.Id,
                    draft.Id,
                    Guid.Empty,
                    content));
            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await Client.Drafts.UpdateDocumentSignatureContentAsync(
                    Account.Id,
                    draft.Id,
                    emptyDocument.Id,
                    null));
        }

        [Test]
        public async Task UpdateSignatureContent_WithValidParameters()
        {
            var content = new byte[] {7};
            var document = await CreateEmptyDocument();
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.UpdateDocumentSignatureContentAsync(
                    Account.Id,
                    draft.Id,
                    document.Id,
                    content));
            await Client.Drafts.DeleteDocumentAsync(Account.Id, draft.Id, document.Id);
        }

        [Test]
        public void FailToCreateSignature_WithBadParameters()
        {
            var request = new SignatureRequest {Base64Content = Convert.ToBase64String(new byte[] {5})};
            var badRequest = new SignatureRequest {Base64Content = "2"};
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.AddDocumentSignatureAsync(
                    Guid.Empty,
                    draft.Id,
                    filledDocument.Id,
                    request));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.AddDocumentSignatureAsync(
                    Account.Id,
                    Guid.Empty,
                    filledDocument.Id,
                    request));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.AddDocumentSignatureAsync(Account.Id, draft.Id, Guid.Empty, request));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.AddDocumentSignatureAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    badRequest));
        }

        [Test]
        public void FailToDeleteSignature_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.DeleteDocumentSignatureAsync(
                    Guid.Empty,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.DeleteDocumentSignatureAsync(
                    Account.Id,
                    Guid.Empty,
                    filledDocument.Id,
                    filledDocumentSignature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.DeleteDocumentSignatureAsync(
                    Account.Id,
                    draft.Id,
                    Guid.Empty,
                    filledDocumentSignature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.DeleteDocumentSignatureAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    Guid.Empty));
        }

        [Test]
        public void DeleteExistingSignature()
        {
            Assert.DoesNotThrowAsync(
                async () =>
                {
                    var signature = await Client.Drafts.AddDocumentSignatureAsync(Account.Id, draft.Id, emptyDocument.Id);
                    await Client.Drafts.DeleteDocumentSignatureAsync(Account.Id, draft.Id, emptyDocument.Id, signature.Id);
                });
        }

        [Test]
        public void FailToGetSignature_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentSignatureAsync(
                    Guid.Empty,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentSignatureAsync(
                    Account.Id,
                    Guid.Empty,
                    filledDocument.Id,
                    filledDocumentSignature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentSignatureAsync(
                    Account.Id,
                    draft.Id,
                    Guid.Empty,
                    filledDocumentSignature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentSignatureAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    Guid.Empty));
        }

        [Test]
        public void GetExistingSignature()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.GetDocumentSignatureAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id));
        }

        [Test]
        public void FailToUpdateSignature_WithBadParameters()
        {
            var validRequest = new SignatureRequest {Base64Content = Convert.ToBase64String(new byte[] {6, 7})};
            var invalidRequest = new SignatureRequest {Base64Content = "2"};
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentSignatureAsync(
                    Guid.Empty,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id,
                    validRequest));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentSignatureAsync(
                    Account.Id,
                    Guid.Empty,
                    filledDocument.Id,
                    filledDocumentSignature.Id,
                    validRequest));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentSignatureAsync(
                    Account.Id,
                    draft.Id,
                    Guid.Empty,
                    filledDocumentSignature.Id,
                    validRequest));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentSignatureAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    Guid.Empty,
                    validRequest));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.UpdateDocumentSignatureAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id,
                    invalidRequest));
        }

        [Test]
        public async Task UpdateSignature_WithValidParameters()
        {
            var signature = await Client.Drafts.AddDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id);
            var newRequest = new SignatureRequest {Base64Content = Convert.ToBase64String(new byte[] {6, 7})};
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.UpdateDocumentSignatureAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    signature.Id,
                    newRequest));
            await Client.Drafts.DeleteDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, signature.Id);
        }

        [Test]
        public void FailToGetSignatureContent_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentSignatureContentAsync(
                    Guid.Empty,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentSignatureContentAsync(
                    Account.Id,
                    Guid.Empty,
                    filledDocument.Id,
                    filledDocumentSignature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentSignatureContentAsync(
                    Account.Id,
                    draft.Id,
                    Guid.Empty,
                    filledDocumentSignature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentSignatureContentAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    Guid.Empty));
        }

        [Test]
        public void GetExistingSignatureContent()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.GetDocumentSignatureContentAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id));
        }

        [Test]
        public void FailToCheckOrPrepareOrSendDraft_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.CheckDraftAsync(Account.Id, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.CheckDraftAsync(Guid.Empty, draft.Id));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.PrepareDraftAsync(Account.Id, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.PrepareDraftAsync(Guid.Empty, draft.Id));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.SendDraftAsync(Account.Id, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.SendDraftAsync(Guid.Empty, draft.Id));
        }

        [Test]
        public void FailToCheckAndPrepareAndSendNotDeferredDraft_WithDocumentsWithoutContent()
        {
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.CheckDraftAsync(Account.Id, draft.Id));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.PrepareDraftAsync(Account.Id, draft.Id));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.SendDraftAsync(Account.Id, draft.Id));
        }

        [Test]
        public async Task FailToCheckAndPrepareAndSendDraft_WithoutDocuments()
        {
            var d = await CreateDraftAsync();
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.CheckDraftAsync(Account.Id, d.Id));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.PrepareDraftAsync(Account.Id, d.Id));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.SendDraftAsync(Account.Id, d.Id));
            await DeleteDraftAsync(d);
        }

        [Test]
        public async Task CheckDraft_WhenDeferredIsTrue()
        {
            var d = await CreateDraftAsync();
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.CheckDraftAsync(Account.Id, d.Id, true));
            await DeleteDraftAsync(d);
        }

        [Test]
        public async Task PrepareDraft_WhenDeferredIsTrue()
        {
            var d = await CreateDraftAsync();
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.PrepareDraftAsync(Account.Id, d.Id, true));
            await DeleteDraftAsync(d);
        }

        [Test]
        public async Task SendDraft_WhenDeferredIsTrue()
        {
            var d = await CreateDraftAsync();
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.SendDraftAsync(Account.Id, d.Id, true));
            await DeleteDraftAsync(d);
        }

        [Test]
        public void FailToGetDocumentEncryptedContent_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentEncryptedContentAsync(Guid.Empty, draft.Id, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentEncryptedContentAsync(Account.Id, Guid.Empty, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentEncryptedContentAsync(Account.Id, draft.Id, Guid.Empty));
        }

        [Test]
        public void FailToGetDocumentEncryptedContent_WhenItIsEmpty()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.GetDocumentEncryptedContentAsync(Account.Id, draft.Id, emptyDocument.Id));
        }

        [Test]
        public void FailToBuildDocument_WithBadParameters()
        {
            var content = JsonConvert.SerializeObject(new DocumentContents());
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.BuildDocumentContentAsync(
                Guid.Empty, draft.Id, filledDocument.Id, FormatType.uSN, content));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.BuildDocumentContentAsync(
                Account.Id, Guid.Empty, filledDocument.Id, FormatType.uSN, content));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.BuildDocumentContentAsync(
                Account.Id, draft.Id, Guid.Empty, FormatType.uSN, content));
        }

        [Test]
        public void FailToCreateDocumentWithContentFromFormat_WithBadParameters()
        {
            var content = JsonConvert.SerializeObject(new DocumentContents());
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDocumentWithContentFromFormatAsync(
                    Guid.Empty, draft.Id, FormatType.uSN, content));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDocumentWithContentFromFormatAsync(
                    Account.Id, Guid.Empty, FormatType.uSN, content));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.Drafts.CreateDocumentWithContentFromFormatAsync(
                    Account.Id, draft.Id, FormatType.uSN, "ha"));
        }

        [Test]
        public void FailToGetDraftTasks_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.GetDraftTasks(Guid.Empty, draft.Id));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.GetDraftTasks(Account.Id, Guid.Empty));
        }

        [Test]
        public void GetDraftTasks_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.GetDraftTasks(Account.Id, draft.Id));
        }

        [Test]
        public async Task FailToGetDraftTask_WithBadParameters()
        {
            var d = await CreateDraftAsync();
            await Client.Drafts.CheckDraftAsync(Account.Id, d.Id, true);
            var taskId = (await Client.Drafts.GetDraftTasks(Account.Id, d.Id)).ApiTaskPageItems[0].Id;
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.GetDraftTask(Guid.Empty, d.Id, taskId));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.GetDraftTask(Account.Id, Guid.Empty, taskId));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.GetDraftTask(Account.Id, d.Id, Guid.Empty));
            await Client.Drafts.DeleteDraftAsync(Account.Id, d.Id);
        }

        [Test]
        public async Task GetDraftTask_WithValidParameters()
        {
            var d = await CreateDraftAsync();
            await Client.Drafts.CheckDraftAsync(Account.Id, d.Id, true);
            var taskId = (await Client.Drafts.GetDraftTasks(Account.Id, d.Id)).ApiTaskPageItems[0].Id;
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.GetDraftTask(Account.Id, d.Id, taskId));
            await Client.Drafts.DeleteDraftAsync(Account.Id, d.Id);
        }

        [Test]
        public async Task FailToCloudSignDraft_WithBadParameters()
        {
            var d = await CreateDraftAsync();
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.CloudSignDraftAsync(Guid.Empty, d.Id));
            Assert.ThrowsAsync<ApiException>(async () => await Client.Drafts.CloudSignDraftAsync(Account.Id, Guid.Empty));
            await DeleteDraftAsync(d);
        }

        //[Test]
        //public async Task Test()
        //{
        //    var d = await CreateDraftAsync();
        //    var doc = await CreateFilledDocument(d);
        //    try
        //    {
        //        var t = await Client.Drafts.CloudSignDraftAsync(Account.Id, d.Id);
        //    }
        //    catch (ApiException)
        //    {
        //    }
        //    await Client.Drafts.DeleteDocumentAsync(Account.Id, d.Id, doc.Id);
        //    await DeleteDraftAsync(d);
        //    Assert.Fail();
        //}

        private async Task<Draft> CreateDraftAsync() => await Client.Drafts.CreateDraftAsync(Account.Id, validDraftMetaRequest);

        private async Task DeleteDraftAsync(Draft d) => await Client.Drafts.DeleteDraftAsync(Account.Id, d.Id);

        private async Task<DraftDocument> CreateEmptyDocument() =>
            await Client.Drafts.AddDocumentAsync(Account.Id, draft.Id, null);

        private async Task<DraftDocument> CreateFilledDocument(Draft d) =>
            await Client.Drafts.AddDocumentAsync(
                Account.Id,
                d.Id,
                new DocumentContents
                {
                    Base64Content = Convert.ToBase64String(new byte[] {1}),
                    Description = new DocumentDescriptionRequestDto
                    {
                        ContentType = "application/json",
                        Filename = "Filename",
                        Type = new Urn("nid", "nss")
                    },
                    Signature = validDraftMetaRequest.Sender.Certificate.Content
                });
    }
}