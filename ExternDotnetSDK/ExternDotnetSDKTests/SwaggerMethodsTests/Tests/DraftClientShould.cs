using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Account;
using ExternDotnetSDK.Clients.Certificates;
using ExternDotnetSDK.Clients.Drafts;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.Drafts;
using ExternDotnetSDK.Drafts.Requests;
using FluentAssertions;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class DraftClientShould : AllTestsShould
    {
        private DraftClient draftClient;
        private DraftMetaRequest validDraftMetaRequest;
        private Draft draft;
        private DraftDocument emptyDocument;
        private DraftDocument filledDocument;
        private List<Signature> filledDocumentSignatures;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await InitializeCommonHttpClient();
            AccountClient = new AccountClient(Client);
            draftClient = new DraftClient(Client);
            Account = (await AccountClient.GetAccountsAsync(0, 1)).Accounts[0];
            var cert = (await new CertificateClient(Client).GetCertificatesAsync(Account.Id, 0, 1)).Certificates[0];
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
            draft = await draftClient.CreateDraftAsync(Account.Id, validDraftMetaRequest);
            emptyDocument = await CreateEmptyDocument();
            filledDocument = await CreateFilledDocument();
            filledDocumentSignatures = new List<Signature>();
        }

        [OneTimeTearDown]
        public override async Task TearDown()
        {
            foreach (var signature in filledDocumentSignatures)
                await draftClient.DeleteDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, signature.Id);
            await draftClient.DeleteDocumentAsync(Account.Id, draft.Id, emptyDocument.Id);
            await draftClient.DeleteDocumentAsync(Account.Id, draft.Id, filledDocument.Id);
            await draftClient.DeleteDraftAsync(Account.Id, draft.Id);
        }

        private async Task<DraftDocument> CreateEmptyDocument()
        {
            return await draftClient.AddDocumentAsync(Account.Id, draft.Id, null);
        }

        private async Task<DraftDocument> CreateFilledDocument()
        {
            return await draftClient.AddDocumentAsync(
                Account.Id,
                draft.Id,
                new DocumentContents
                {
                    Base64Content = Convert.ToBase64String(new byte[]{1,2,3,4}),
                    Description = new DocumentDescriptionRequestDto
                    {
                        ContentType = "application/json",
                        Filename = "Filename",
                        Type = new Urn("nid","nss")
                    },
                    Signature = validDraftMetaRequest.Sender.Certificate.Content
                });
        }

        [Test]
        public void FailToCreateDraft_WithMissingDraftMetaRequest()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.CreateDraftAsync(Account.Id, null));
        }

        [Test]
        public void FailToCreateDraft_WithMissingPayer()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.CreateDraftAsync(Account.Id, new DraftMetaRequest()));
        }

        [Test]
        public void FailToCreateDraft_WithMissingSender()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest
                    {
                        Payer = new AccountInfoRequest()
                    }));
        }

        [Test]
        public void FailToCreateDraft_WithMissingRecipient()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.CreateDraftAsync(
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
                async () => await draftClient.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest
                    {
                        Payer = new AccountInfoRequest(),
                        Sender = new SenderRequest(),
                        Recipient = new RecipientInfoRequest { FssCode = "11111" }
                    }));
        }

        [Test]
        public void FailToCreateDraft_WithMissingIp()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.CreateDraftAsync(
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
                async () => await draftClient.CreateDraftAsync(
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
                async () => await draftClient.CreateDraftAsync(
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
                async () => await draftClient.CreateDraftAsync(
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
                async () => await draftClient.CreateDraftAsync(
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
                async () => await draftClient.CreateDraftAsync(
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
                async () => await draftClient.DeleteDraftAsync(Account.Id, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.DeleteDraftAsync(Guid.Empty, draft.Id));
        }

        [Test]
        public void FailToGetDraft_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDraftAsync(Account.Id, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDraftAsync(Guid.Empty, draft.Id));
        }

        [Test]
        public void GetDraft_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await draftClient.GetDraftAsync(Account.Id, draft.Id));
        }

        [Test]
        public void GetDraftMeta_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await draftClient.GetDraftMetaAsync(Account.Id, draft.Id));
        }

        [Test]
        public void FailToGetDraftMeta_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDraftMetaAsync(Account.Id, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDraftMetaAsync(Guid.Empty, draft.Id));
        }

        [Test]
        public void UpdateDraftMeta_WithAnyParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await draftClient.UpdateDraftMetaAsync(Account.Id, draft.Id, validDraftMetaRequest));
            Assert.DoesNotThrowAsync(
                async () => await draftClient.UpdateDraftMetaAsync(Account.Id, draft.Id, null));
        }

        [Test]
        public async Task ReturnDifferentDraftMeta_WhenUpdatedWithValidAndDifferentParameters()
        {
            var oldMeta = await draftClient.UpdateDraftMetaAsync(Account.Id, draft.Id, validDraftMetaRequest);
            var newMeta = await draftClient.UpdateDraftMetaAsync(
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
            var oldMeta = await draftClient.UpdateDraftMetaAsync(Account.Id, draft.Id, validDraftMetaRequest);
            var newMeta = await draftClient.UpdateDraftMetaAsync(Account.Id, draft.Id, null);
            oldMeta.Should().BeEquivalentTo(newMeta);
        }

        [Test]
        public async Task DeleteDocument_WithValidParameters()
        {
            var document = await CreateEmptyDocument();
            Assert.DoesNotThrowAsync(
                async () => await draftClient.DeleteDocumentAsync(Account.Id, draft.Id, document.Id));
        }

        [Test]
        public void FailToDeleteDocument_WithInvalidParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.DeleteDocumentAsync(Guid.Empty, draft.Id, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.DeleteDocumentAsync(Account.Id, Guid.Empty, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.DeleteDocumentAsync(Account.Id, draft.Id, Guid.Empty));
        }

        [Test]
        public void FailToCreateDocument_WithWrongBase64Content()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.AddDocumentAsync(
                    Account.Id,
                    draft.Id,
                    new DocumentContents {Base64Content = "1"}));
        }

        [Test]
        public void FailToCreateDocument_WithWrongSignature()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.AddDocumentAsync(
                    Account.Id,
                    draft.Id,
                    new DocumentContents {Signature = "1"}));
        }

        [Test]
        public void GetDocument_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await draftClient.GetDocumentAsync(Account.Id, draft.Id, emptyDocument.Id));
        }

        [Test]
        public void FailToGetDocument_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentAsync(Guid.Empty, draft.Id, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentAsync(Account.Id, Guid.Empty, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentAsync(Account.Id, draft.Id, Guid.Empty));
        }

        [Test]
        public void FailToUpdateDocument_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.UpdateDocumentAsync(
                    Account.Id,
                    draft.Id,
                    emptyDocument.Id,
                    new DocumentContents {Base64Content = "1"}));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.UpdateDocumentAsync(
                    Account.Id,
                    draft.Id,
                    emptyDocument.Id,
                    new DocumentContents {Signature = "1"}));
        }

        [Test]
        public async Task ReturnDifferentDocument_WhenUpdatedWithValidAndDifferentParameters()
        {
            var newDoc = await draftClient.UpdateDocumentAsync(
                Account.Id,
                draft.Id,
                filledDocument.Id,
                new DocumentContents
                {
                    Base64Content = Convert.ToBase64String(new byte[] {1})
                });
            newDoc.Should().NotBeEquivalentTo(filledDocument);
        }

        [Test]
        public void FailToGetDocumentPrint_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentPrintAsync(Guid.Empty, draft.Id, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentPrintAsync(Account.Id, Guid.Empty, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentPrintAsync(Account.Id, draft.Id, Guid.Empty));
        }

        [Test]
        public void FailToGetNonexistentDocumentPrint()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentPrintAsync(Account.Id, draft.Id, emptyDocument.Id));
        }

        [Test]
        public void FailToGetDocumentDecryptedContent_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentDecryptedContentAsync(Guid.Empty, draft.Id, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentDecryptedContentAsync(Account.Id, Guid.Empty, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentDecryptedContentAsync(Account.Id, draft.Id, Guid.Empty));
        }

        [Test]
        public void FailToGetDocumentDecryptedContent_WhenItIsEmpty()
        {
            Assert.ThrowsAsync<ApiException>(async () =>
                await draftClient.GetDocumentDecryptedContentAsync(Account.Id, draft.Id, emptyDocument.Id));
        }

        [Test]
        public void GetDocumentDecryptedContent_WhenItExists()
        {
            Assert.DoesNotThrowAsync(async () => 
                await draftClient.GetDocumentDecryptedContentAsync(Account.Id, draft.Id, filledDocument.Id));
        }

        [Test]
        public void FailToUpdateDocumentDecryptedContent_WithBadParameters()
        {
            var content = new byte[] {9, 1, 1};
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.UpdateDocumentDecryptedContentAsync(Guid.Empty, draft.Id, emptyDocument.Id, content));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.UpdateDocumentDecryptedContentAsync(Account.Id, Guid.Empty, emptyDocument.Id, content));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.UpdateDocumentDecryptedContentAsync(Account.Id, draft.Id, Guid.Empty, content));
            Assert.ThrowsAsync<ArgumentNullException>(async () => 
                await draftClient.UpdateDocumentDecryptedContentAsync(Account.Id, draft.Id, emptyDocument.Id, null));
        }

        [Test]
        public void UpdateDocumentDecryptedContent_WithValidParameters()
        {
            var content = new byte[] {1};
            Assert.DoesNotThrowAsync(async () => 
                await draftClient.UpdateDocumentDecryptedContentAsync(Account.Id, draft.Id, filledDocument.Id, content));
        }

        [Test]
        public void FailToGetDocumentSignatureContent_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentSignatureContentAsync(Guid.Empty, draft.Id, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentSignatureContentAsync(Account.Id, Guid.Empty, emptyDocument.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentSignatureContentAsync(Account.Id, draft.Id, Guid.Empty));
        }

        [Test]
        public void FailToGetDocumentSignatureContent_WhenItIsEmpty()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentSignatureContentAsync(Account.Id, draft.Id, emptyDocument.Id));
        }

        [Test]
        public void GetDocumentSignatureContent_WhenItExists()
        {
            Assert.DoesNotThrowAsync(
                async () => await draftClient.GetDocumentSignatureContentAsync(Account.Id, draft.Id, filledDocument.Id));
        }

        [Test]
        public void FailToUpdateSignatureContent_WithBadParameters()
        {
            var content = new byte[] {1};
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.UpdateDocumentSignatureContentAsync(Guid.Empty, draft.Id, emptyDocument.Id, content));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.UpdateDocumentSignatureContentAsync(Account.Id, Guid.Empty, emptyDocument.Id, content));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.UpdateDocumentSignatureContentAsync(Account.Id, draft.Id, Guid.Empty, content));
            Assert.ThrowsAsync<ArgumentNullException>(async () => 
                await draftClient.UpdateDocumentSignatureContentAsync(Account.Id, draft.Id, emptyDocument.Id, null));
        }

        [Test]
        public void UpdateSignatureContent_WithValidParameters()
        {
            var content = new byte[] {7};
            Assert.DoesNotThrowAsync(async () =>
                await draftClient.UpdateDocumentSignatureContentAsync(Account.Id, draft.Id, filledDocument.Id, content));
        }

        [Test]
        public void FailToCreateSignature_WithBadParameters()
        {
            var request = new SignatureRequest
            {
                Base64Content = Convert.ToBase64String(new byte[] {5})
            };
            Assert.ThrowsAsync<ApiException>(
                async () => filledDocumentSignatures.Add(
                    await draftClient.AddDocumentSignatureAsync(Guid.Empty, draft.Id, filledDocument.Id, request)));
            Assert.ThrowsAsync<ApiException>(
                async () => filledDocumentSignatures.Add(
                    await draftClient.AddDocumentSignatureAsync(Account.Id, Guid.Empty, filledDocument.Id, request)));
            Assert.ThrowsAsync<ApiException>(
                async () => filledDocumentSignatures.Add(
                    await draftClient.AddDocumentSignatureAsync(Account.Id, draft.Id, Guid.Empty, request)));
        }

        [Test]
        public void CreateSignature_WithEmptySignatureRequest()
        {
            Assert.DoesNotThrowAsync(
                async () => filledDocumentSignatures.Add(
                    await draftClient.AddDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, null)));
        }

        [Test]
        public async Task FailToDeleteSignature_WithBadParameters()
        {
            var signature = await draftClient.AddDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, null);
            filledDocumentSignatures.Add(signature);
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.DeleteDocumentSignatureAsync(Guid.Empty, draft.Id, filledDocument.Id, signature.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.DeleteDocumentSignatureAsync(Account.Id, Guid.Empty, filledDocument.Id, signature.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.DeleteDocumentSignatureAsync(Account.Id, draft.Id, Guid.Empty, signature.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.DeleteDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, Guid.Empty));
        }

        [Test]
        public void DeleteExistingSignature()
        {
            Assert.DoesNotThrowAsync(
                async () =>
                {
                    var signature = await draftClient.AddDocumentSignatureAsync(Account.Id, draft.Id, emptyDocument.Id, null);
                    await draftClient.DeleteDocumentSignatureAsync(Account.Id, draft.Id, emptyDocument.Id, signature.Id);
                });
        }

        [Test]
        public async Task FailToGetSignature_WithBadParameters()
        {
            var signature = await draftClient.AddDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, null);
            filledDocumentSignatures.Add(signature);
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentSignatureAsync(Guid.Empty, draft.Id, filledDocument.Id, signature.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentSignatureAsync(Account.Id, Guid.Empty, filledDocument.Id, signature.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentSignatureAsync(Account.Id, draft.Id, Guid.Empty, signature.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, Guid.Empty));
        }

        [Test]
        public async Task GetExistingSignature()
        {
            var signature = await draftClient.AddDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, null);
            filledDocumentSignatures.Add(signature);
            Assert.DoesNotThrowAsync(async () => 
                await draftClient.GetDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, signature.Id));
        }

        [Test]
        public async Task FailToUpdateSignature_WithBadParameters()
        {
            var signature = await draftClient.AddDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, null);
            filledDocumentSignatures.Add(signature);
            var validRequest = new SignatureRequest {Base64Content = Convert.ToBase64String(new byte[] {6, 7})};
            var invalidRequest = new SignatureRequest {Base64Content = "2"};
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.UpdateDocumentSignatureAsync(
                    Guid.Empty, draft.Id, filledDocument.Id, signature.Id, validRequest));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.UpdateDocumentSignatureAsync(
                    Account.Id, Guid.Empty, filledDocument.Id, signature.Id, validRequest));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.UpdateDocumentSignatureAsync(
                    Account.Id, draft.Id, Guid.Empty, signature.Id, validRequest));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.UpdateDocumentSignatureAsync(
                    Account.Id, draft.Id, filledDocument.Id, Guid.Empty, validRequest));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.UpdateDocumentSignatureAsync(
                    Account.Id, draft.Id, filledDocument.Id, signature.Id, invalidRequest));
        }

        [Test]
        public async Task UpdateSignature_WithValidParameters()
        {
            var signature = await draftClient.AddDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, null);
            filledDocumentSignatures.Add(signature);
            var newRequest = new SignatureRequest {Base64Content = Convert.ToBase64String(new byte[] {6, 7})};
            Assert.DoesNotThrowAsync(async () => await draftClient.UpdateDocumentSignatureAsync(
                Account.Id, draft.Id, filledDocument.Id, signature.Id, newRequest));
        }

        [Test]
        public async Task FailToGetSignatureContent_WithBadParameters()
        {
            var signature = await draftClient.AddDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, null);
            filledDocumentSignatures.Add(signature);
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentSignatureContentAsync(Guid.Empty, draft.Id, filledDocument.Id, signature.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentSignatureContentAsync(Account.Id, Guid.Empty, filledDocument.Id, signature.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentSignatureContentAsync(Account.Id, draft.Id, Guid.Empty, signature.Id));
            Assert.ThrowsAsync<ApiException>(async () => 
                await draftClient.GetDocumentSignatureContentAsync(Account.Id, draft.Id, filledDocument.Id, Guid.Empty));
        }

        [Test]
        public async Task GetExistingSignatureContent()
        {
            var signature = await draftClient.AddDocumentSignatureAsync(Account.Id, draft.Id, filledDocument.Id, null);
            filledDocumentSignatures.Add(signature);
            Assert.DoesNotThrowAsync(async () => 
                await draftClient.GetDocumentSignatureContentAsync(Account.Id, draft.Id, filledDocument.Id, signature.Id));
        }
    }
}