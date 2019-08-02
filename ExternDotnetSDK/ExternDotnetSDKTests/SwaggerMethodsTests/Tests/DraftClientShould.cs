using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Account;
using ExternDotnetSDK.Clients.Certificates;
using ExternDotnetSDK.Clients.Drafts;
using ExternDotnetSDK.Drafts;
using ExternDotnetSDK.Drafts.Requests;
using FluentAssertions;
using NUnit.Framework;
using Refit;
#pragma warning disable 4014
#pragma warning disable 1998

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class DraftClientShould : AllTestsShould
    {
        private DraftClient draftClient;
        private DraftMetaRequest validDraftMetaRequest;
        private Draft createdDraft;
        private List<DraftDocument> createdDocuments = new List<DraftDocument>();

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
            createdDraft = await draftClient.CreateDraftAsync(Account.Id, validDraftMetaRequest);
        }

        [OneTimeTearDown]
        public override async Task TearDown()
        {
            foreach (var document in createdDocuments)
                await draftClient.DeleteDocumentAsync(Account.Id, createdDraft.Id, document.Id);
            await draftClient.DeleteDraftAsync(Account.Id, createdDraft.Id);
        }

        private async Task<DraftDocument> CreateEmptyDocument()
        {
            var doc = await draftClient.AddDocumentAsync(Account.Id, createdDraft.Id, null);
            createdDocuments.Add(doc);
            return doc;
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
                        Recipient = new RecipientInfoRequest {FssCode = "11111"}
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
        // ReSharper disable once IdentifierTypo
        public void FailToCreateDraft_WithUnconvertableSenderCertificate()
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
                async () => await draftClient.DeleteDraftAsync(Guid.Empty, createdDraft.Id));
        }

        [Test]
        public void FailToGetDraft_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDraftAsync(Account.Id, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDraftAsync(Guid.Empty, createdDraft.Id));
        }

        [Test]
        public void GetDraft_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await draftClient.GetDraftAsync(Account.Id, createdDraft.Id));
        }

        [Test]
        public void GetDraftMeta_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await draftClient.GetDraftMetaAsync(Account.Id, createdDraft.Id));
        }

        [Test]
        public void FailToGetDraftMeta_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDraftMetaAsync(Account.Id, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDraftMetaAsync(Guid.Empty, createdDraft.Id));
        }

        [Test]
        public void UpdateDraftMeta_WithAnyParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await draftClient.UpdateDraftMetaAsync(Account.Id, createdDraft.Id, validDraftMetaRequest));
            Assert.DoesNotThrowAsync(
                async () => await draftClient.UpdateDraftMetaAsync(Account.Id, createdDraft.Id, null));
        }

        [Test]
        public async Task ReturnDifferentDraftMeta_WhenUpdatedWithValidAndDifferentParameters()
        {
            var oldMeta = await draftClient.UpdateDraftMetaAsync(Account.Id, createdDraft.Id, validDraftMetaRequest);
            var newMeta = await draftClient.UpdateDraftMetaAsync(
                Account.Id,
                createdDraft.Id,
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
            var oldMeta = await draftClient.UpdateDraftMetaAsync(Account.Id, createdDraft.Id, validDraftMetaRequest);
            var newMeta = await draftClient.UpdateDraftMetaAsync(Account.Id, createdDraft.Id, null);
            oldMeta.Should().BeEquivalentTo(newMeta);
        }

        [Test]
        public async Task DeleteDocument_WithValidParameters()
        {
            var document = await CreateEmptyDocument();
            Assert.DoesNotThrowAsync(
                async () => await draftClient.DeleteDocumentAsync(Account.Id, createdDraft.Id, document.Id));
        }

        [Test]
        public async Task FailToDeleteDocument_WithInvalidParameters()
        {
            var document = await CreateEmptyDocument();
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.DeleteDocumentAsync(Guid.Empty, createdDraft.Id, document.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.DeleteDocumentAsync(Account.Id, Guid.Empty, document.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.DeleteDocumentAsync(Account.Id, createdDraft.Id, Guid.Empty));
        }

        [Test]
        public void CreateEmptyDocument_WithEmptyContent()
        {
            Assert.DoesNotThrowAsync(
                async () => createdDocuments.Add(
                    await draftClient.AddDocumentAsync(Account.Id, createdDraft.Id, null)));
        }

        [Test]
        public void FailToCreateDocument_WithWrongBase64Content()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.AddDocumentAsync(
                    Account.Id,
                    createdDraft.Id,
                    new DocumentContents {Base64Content = "1"}));
        }

        [Test]
        public void FailToCreateDocument_WithWrongSignature()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.AddDocumentAsync(
                    Account.Id,
                    createdDraft.Id,
                    new DocumentContents {Signature = "1"}));
        }

        [Test]
        public async Task GetDocument_WithValidParameters()
        {
            var doc = await CreateEmptyDocument();
            Assert.DoesNotThrowAsync(
                async () => await draftClient.GetDocumentAsync(Account.Id, createdDraft.Id, doc.Id));
        }

        [Test]
        public async Task FailToGetDocument_WithBadParameters()
        {
            var doc = await CreateEmptyDocument();
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentAsync(Guid.Empty, createdDraft.Id, doc.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentAsync(Account.Id, Guid.Empty, doc.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentAsync(Account.Id, createdDraft.Id, Guid.Empty));
        }

        [Test]
        public async Task FailToUpdateDocument_WithBadParameters()
        {
            var doc = await CreateEmptyDocument();
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.UpdateDocumentAsync(
                    Account.Id,
                    createdDraft.Id,
                    doc.Id,
                    new DocumentContents {Base64Content = "1"}));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.UpdateDocumentAsync(
                    Account.Id,
                    createdDraft.Id,
                    doc.Id,
                    new DocumentContents {Signature = "1"}));
        }

        [Test]
        public async Task ReturnDifferentDocument_WhenUpdatedWithValidAndDifferentParameters()
        {
            var doc = await CreateEmptyDocument();
            var newDoc = await draftClient.UpdateDocumentAsync(
                Account.Id,
                createdDraft.Id,
                doc.Id,
                new DocumentContents
                {
                    Base64Content = Convert.ToBase64String(new byte[] {1})
                });
            newDoc.Should().NotBeEquivalentTo(doc);
        }

        [Test]
        public async Task FailToGetDocumentPrint_WithBadParameters()
        {
            var doc = await CreateEmptyDocument();
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentPrintAsync(Guid.Empty, createdDraft.Id, doc.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentPrintAsync(Account.Id, Guid.Empty, doc.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentPrintAsync(Account.Id, createdDraft.Id, Guid.Empty));
        }

        [Test]
        public async Task FailToGetNonexistentDocumentPrint()
        {
            var doc = await CreateEmptyDocument();
            Assert.ThrowsAsync<ApiException>(
                async () => await draftClient.GetDocumentPrintAsync(Account.Id, createdDraft.Id, doc.Id));
        }


    }
}