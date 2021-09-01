using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Documents;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Signatures;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class DraftsClientShould : AllTestsShould
    {
        private DraftMetaRequest validDraftMetaRequest;
        private Draft draft;
        private DraftDocument someDocument;
        private DraftDocument filledDocument;
        private Signature filledDocumentSignature;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            InitializeClient();
            Account = (await Client.Accounts.GetAccountsAsync(0, 1).ConfigureAwait(false)).Accounts[0];
            var cert = (await Client.Accounts.GetAccountCertificatesAsync(Account.Id).ConfigureAwait(false)).Certificates[0];
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
                    Certificate = new CertificateRequest {PublicKey = cert.Content}
                },
                Recipient = new RecipientInfoRequest {FssCode = "11111"}
            };
            draft = await CreateDraftAsync().ConfigureAwait(false);
            someDocument = await CreateFilledDocument().ConfigureAwait(false);
            filledDocument = await CreateFilledDocument(draft).ConfigureAwait(false);
            filledDocumentSignature = await Client.Drafts.CreateSignatureAsync(
                Account.Id,
                draft.Id,
                filledDocument.Id,
                new SignatureRequest {Base64Content = Convert.ToBase64String(new byte[] {1, 2, 3, 4})}).ConfigureAwait(false);
        }

        [OneTimeTearDown]
        public override async Task TearDown()
        {
            await Client.Drafts.DeleteSignatureAsync(
                Account.Id,
                draft.Id,
                filledDocument.Id,
                filledDocumentSignature.Id).ConfigureAwait(false);
            await Client.Drafts.DeleteDocumentAsync(Account.Id, draft.Id, someDocument.Id).ConfigureAwait(false);
            await Client.Drafts.DeleteDocumentAsync(Account.Id, draft.Id, filledDocument.Id).ConfigureAwait(false);
            await Client.Drafts.DeleteDraftAsync(Account.Id, draft.Id).ConfigureAwait(false);
        }

        [Test]
        public void FailToCreateDraft_WithMissingDraftMetaRequest()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.CreateDraftAsync(Account.Id, null).ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateDraft_WithMissingPayer()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.CreateDraftAsync(Account.Id, new DraftMetaRequest()).ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateDraft_WithMissingSender()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest {Payer = new AccountInfoRequest()}).ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateDraft_WithMissingRecipient()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest
                    {
                        Payer = new AccountInfoRequest(),
                        Sender = new SenderRequest()
                    }).ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateDraft_WithMissingInnKpp()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.CreateDraftAsync(
                    Account.Id,
                    new DraftMetaRequest
                    {
                        Payer = new AccountInfoRequest(),
                        Sender = new SenderRequest(),
                        Recipient = new RecipientInfoRequest {FssCode = "11111"}
                    }).ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateDraft_WithMissingIp()
        {
            Assert.ThrowsAsync<HttpRequestException>(
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
                    }).ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateDraft_WithMissingSenderCertificate()
        {
            Assert.ThrowsAsync<HttpRequestException>(
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
                    }).ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateDraft_WithEmptySenderCertificate()
        {
            Assert.ThrowsAsync<HttpRequestException>(
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
                    }).ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateDraft_WithInconvertableSenderCertificate()
        {
            Assert.ThrowsAsync<HttpRequestException>(
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
                                Certificate = new CertificateRequest {PublicKey = new byte[] {1, 2, 3}}
                            },
                            Recipient = validDraftMetaRequest.Recipient
                        })
                    .ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateDraft_WithSenderCertificateOfWrongOrganization()
        {
            Assert.ThrowsAsync<HttpRequestException>(
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
                    }).ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateDraft_WithMissingSenderOrganization()
        {
            Assert.ThrowsAsync<HttpRequestException>(
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
                    }).ConfigureAwait(false));
        }

        [Test]
        public void FailToDeleteDraft_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.DeleteDraftAsync(Account.Id, Guid.Empty).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.DeleteDraftAsync(Guid.Empty, draft.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToGetDraft_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetDraftAsync(Account.Id, Guid.Empty).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetDraftAsync(Guid.Empty, draft.Id).ConfigureAwait(false));
        }

        [Test]
        public void GetDraft_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.GetDraftAsync(Account.Id, draft.Id).ConfigureAwait(false));
        }

        [Test]
        public void GetDraftMeta_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.GetDraftMetaAsync(Account.Id, draft.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToGetDraftMeta_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetDraftMetaAsync(Account.Id, Guid.Empty).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetDraftMetaAsync(Guid.Empty, draft.Id).ConfigureAwait(false));
        }

        [Test]
        public void UpdateDraftMeta_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.UpdateDraftMetaAsync(Account.Id, draft.Id, validDraftMetaRequest).ConfigureAwait(false));
        }

        [Test]
        public async Task ReturnDifferentDraftMeta_WhenUpdatedWithValidAndDifferentParameters()
        {
            var oldMeta = await Client.Drafts.UpdateDraftMetaAsync(Account.Id, draft.Id, validDraftMetaRequest).ConfigureAwait(false);
            var newMeta = await Client.Drafts.UpdateDraftMetaAsync(
                Account.Id,
                draft.Id,
                new DraftMetaRequest
                {
                    Payer = validDraftMetaRequest.Payer,
                    Sender = validDraftMetaRequest.Sender,
                    Recipient = new RecipientInfoRequest {FssCode = "22222"}
                }).ConfigureAwait(false);
            oldMeta.Should().NotBeEquivalentTo(newMeta);
        }

        [Test]
        public async Task DeleteDocument_WithValidParameters()
        {
            var document = await CreateFilledDocument().ConfigureAwait(false);
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.DeleteDocumentAsync(Account.Id, draft.Id, document.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToDeleteDocument_WithInvalidParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.DeleteDocumentAsync(Guid.Empty, draft.Id, someDocument.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.DeleteDocumentAsync(Account.Id, Guid.Empty, someDocument.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.DeleteDocumentAsync(Account.Id, draft.Id, Guid.Empty).ConfigureAwait(false));
        }

        // [Test]
        // public void FailToCreateDocument_WithWrongBase64Content()
        // {
        //     Assert.ThrowsAsync<HttpRequestException>(
        //         async () => await Client.Drafts.CreateDocumentAsync(
        //             Account.Id,
        //             draft.Id,
        //             new DocumentRequest {Base64Content = "1"}).ConfigureAwait(false));
        // }

        [Test]
        public void GetDocument_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.GetDocumentAsync(Account.Id, draft.Id, someDocument.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToGetDocument_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetDocumentAsync(Guid.Empty, draft.Id, someDocument.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetDocumentAsync(Account.Id, Guid.Empty, someDocument.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetDocumentAsync(Account.Id, draft.Id, Guid.Empty).ConfigureAwait(false));
        }

        // [Test]
        // public async Task UpdateDocument_WithValidParameters()
        // {
        //     var document = await CreateFilledDocument().ConfigureAwait(false);
        //     Assert.DoesNotThrowAsync(
        //         async () => await Client.Drafts.UpdateDocumentAsync(
        //             Account.Id,
        //             draft.Id,
        //             document.Id,
        //             new DocumentRequest {Base64Content = Convert.ToBase64String(new byte[] {1})}).ConfigureAwait(false));
        //     await Client.Drafts.DeleteDocumentAsync(Account.Id, draft.Id, document.Id).ConfigureAwait(false);
        // }

        [Test]
        public void FailToGetDocumentPrint_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.PrintDocumentAsync(Guid.Empty, draft.Id, someDocument.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.PrintDocumentAsync(Account.Id, Guid.Empty, someDocument.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.PrintDocumentAsync(Account.Id, draft.Id, Guid.Empty).ConfigureAwait(false));
        }

        [Test]
        public void FailToGetNonexistentDocumentPrint()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.PrintDocumentAsync(Account.Id, draft.Id, someDocument.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateSignature_WithBadParameters()
        {
            var request = new SignatureRequest {Base64Content = Convert.ToBase64String(new byte[] {5})};
            var badRequest = new SignatureRequest {Base64Content = "2"};
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.CreateSignatureAsync(
                    Guid.Empty,
                    draft.Id,
                    filledDocument.Id,
                    request).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.CreateSignatureAsync(
                    Account.Id,
                    Guid.Empty,
                    filledDocument.Id,
                    request).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.CreateSignatureAsync(Account.Id, draft.Id, Guid.Empty, request).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.CreateSignatureAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    badRequest).ConfigureAwait(false));
        }

        [Test]
        public void FailToDeleteSignature_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.DeleteSignatureAsync(
                    Guid.Empty,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.DeleteSignatureAsync(
                    Account.Id,
                    Guid.Empty,
                    filledDocument.Id,
                    filledDocumentSignature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.DeleteSignatureAsync(
                    Account.Id,
                    draft.Id,
                    Guid.Empty,
                    filledDocumentSignature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.DeleteSignatureAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    Guid.Empty).ConfigureAwait(false));
        }

        // [Test]
        // public void DeleteExistingSignature()
        // {
        //     Assert.DoesNotThrowAsync(
        //         async () =>
        //         {
        //             var signature = await Client.Drafts.CreateSignatureAsync(Account.Id, draft.Id, someDocument.Id).ConfigureAwait(false);
        //             await Client.Drafts.DeleteSignatureAsync(Account.Id, draft.Id, someDocument.Id, signature.Id).ConfigureAwait(false);
        //         });
        // }

        [Test]
        public void FailToGetSignature_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetSignatureAsync(
                    Guid.Empty,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetSignatureAsync(
                    Account.Id,
                    Guid.Empty,
                    filledDocument.Id,
                    filledDocumentSignature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetSignatureAsync(
                    Account.Id,
                    draft.Id,
                    Guid.Empty,
                    filledDocumentSignature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetSignatureAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    Guid.Empty).ConfigureAwait(false));
        }

        [Test]
        public void GetExistingSignature()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.GetSignatureAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToUpdateSignature_WithBadParameters()
        {
            var validRequest = new SignatureRequest {Base64Content = Convert.ToBase64String(new byte[] {6, 7})};
            var invalidRequest = new SignatureRequest {Base64Content = "2"};
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.UpdateSignatureAsync(
                    Guid.Empty,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id,
                    validRequest).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.UpdateSignatureAsync(
                    Account.Id,
                    Guid.Empty,
                    filledDocument.Id,
                    filledDocumentSignature.Id,
                    validRequest).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.UpdateSignatureAsync(
                    Account.Id,
                    draft.Id,
                    Guid.Empty,
                    filledDocumentSignature.Id,
                    validRequest).ConfigureAwait(false));
            // Assert.ThrowsAsync<HttpRequestException>(
            //     async () => await Client.Drafts.UpdateSignatureAsync(
            //         Account.Id,
            //         draft.Id,
            //         filledDocument.Id,
            //         Guid.Empty,
            //         validRequest).ConfigureAwait(false));
            // Assert.ThrowsAsync<HttpRequestException>(
            //     async () => await Client.Drafts.UpdateSignatureAsync(
            //         Account.Id,
            //         draft.Id,
            //         filledDocument.Id,
            //         filledDocumentSignature.Id,
            //         invalidRequest).ConfigureAwait(false));
        }

        // [Test]
        // public async Task UpdateSignature_WithValidParameters()
        // {
        //     var signature = await Client.Drafts.CreateSignatureAsync(Account.Id, draft.Id, filledDocument.Id).ConfigureAwait(false);
        //     var newRequest = new SignatureRequest {Base64Content = Convert.ToBase64String(new byte[] {6, 7})};
        //     Assert.DoesNotThrowAsync(
        //         async () => await Client.Drafts.UpdateSignatureAsync(
        //             Account.Id,
        //             draft.Id,
        //             filledDocument.Id,
        //             signature.Id,
        //             newRequest).ConfigureAwait(false));
        //     await Client.Drafts.DeleteSignatureAsync(Account.Id, draft.Id, filledDocument.Id, signature.Id).ConfigureAwait(false);
        // }

        [Test]
        public void FailToGetSignatureContent_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetSignatureContentAsync(
                    Guid.Empty,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetSignatureContentAsync(
                    Account.Id,
                    Guid.Empty,
                    filledDocument.Id,
                    filledDocumentSignature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetSignatureContentAsync(
                    Account.Id,
                    draft.Id,
                    Guid.Empty,
                    filledDocumentSignature.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetSignatureContentAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    Guid.Empty).ConfigureAwait(false));
        }

        [Test]
        public void GetExistingSignatureContent()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Drafts.GetSignatureContentAsync(
                    Account.Id,
                    draft.Id,
                    filledDocument.Id,
                    filledDocumentSignature.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToCheckOrPrepareOrSendDraft_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.CheckDraftAsync(Account.Id, Guid.Empty).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.CheckDraftAsync(Guid.Empty, draft.Id).ConfigureAwait(false));
            // Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.PrepareDraftAsync(Account.Id, Guid.Empty).ConfigureAwait(false));
            // Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.PrepareDraftAsync(Guid.Empty, draft.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.SendDraftAsync(Account.Id, Guid.Empty).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.SendDraftAsync(Guid.Empty, draft.Id).ConfigureAwait(false));
        }

        // [Test]
        // public void FailToCheckAndPrepareAndSendNotDeferredDraft_WithDocumentsWithoutContent()
        // {
        //     Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.CheckDraftAsync(Account.Id, draft.Id).ConfigureAwait(false));
        //     Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.PrepareDraftAsync(Account.Id, draft.Id).ConfigureAwait(false));
        //     Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.SendDraftAsync(Account.Id, draft.Id).ConfigureAwait(false));
        // }

        [Test]
        public async Task FailToCheckAndPrepareAndSendDraft_WithoutDocuments()
        {
            var d = await CreateDraftAsync().ConfigureAwait(false);
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.CheckDraftAsync(Account.Id, d.Id).ConfigureAwait(false));
            //Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.PrepareDraftAsync(Account.Id, d.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.SendDraftAsync(Account.Id, d.Id).ConfigureAwait(false));
            await DeleteDraftAsync(d).ConfigureAwait(false);
        }

        [Test]
        public async Task StartCheckDraft_WithValidParameters()
        {
            var d = await CreateDraftAsync().ConfigureAwait(false);
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.StartCheckDraftAsync(Account.Id, d.Id).ConfigureAwait(false));
            await DeleteDraftAsync(d).ConfigureAwait(false);
        }

        [Test]
        public async Task StartPrepareDraft_WithValidParameters()
        {
            var d = await CreateDraftAsync().ConfigureAwait(false);
            //Assert.DoesNotThrowAsync(async () => await Client.Drafts.StartPrepareDraftAsync(Account.Id, d.Id).ConfigureAwait(false));
            await DeleteDraftAsync(d).ConfigureAwait(false);
        }

        [Test]
        public async Task StartSendDraft_WithValidParameters()
        {
            var d = await CreateDraftAsync().ConfigureAwait(false);
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.StartSendDraftAsync(Account.Id, d.Id, true).ConfigureAwait(false));
            await DeleteDraftAsync(d).ConfigureAwait(false);
        }

        [Test]
        public void FailToBuildDocument_WithBadParameters()
        {
            var content = JsonConvert.SerializeObject(new DocumentRequest());
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.BuildDocumentAsync(
                    Guid.Empty,
                    draft.Id,
                    filledDocument.Id,
                    DocumentFormatType.USN,
                    1,
                    content).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.BuildDocumentAsync(
                    Account.Id,
                    Guid.Empty,
                    filledDocument.Id,
                    DocumentFormatType.USN,
                    1,
                    content).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.BuildDocumentAsync(
                    Account.Id,
                    draft.Id,
                    Guid.Empty,
                    DocumentFormatType.USN,
                    1,
                    content).ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateDocumentWithContentFromFormat_WithBadParameters()
        {
            var content = JsonConvert.SerializeObject(new DocumentRequest());
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.BuildDocumentAsync(
                    Guid.Empty,
                    draft.Id,
                    DocumentFormatType.USN,
                    1,
                    content).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.BuildDocumentAsync(
                    Account.Id,
                    Guid.Empty,
                    DocumentFormatType.USN,
                    1,
                    content).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.BuildDocumentAsync(
                    Account.Id,
                    draft.Id,
                    DocumentFormatType.USN,
                    1,
                    "ha").ConfigureAwait(false));
        }

        [Test]
        public void FailToGetDraftTasks_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.GetDraftTasks(Guid.Empty, draft.Id).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.GetDraftTasks(Account.Id, Guid.Empty).ConfigureAwait(false));
        }

        [Test]
        public void GetDraftTasks_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.GetDraftTasks(Account.Id, draft.Id).ConfigureAwait(false));
        }

        [Test]
        public async Task FailToGetDraftTask_WithBadParameters()
        {
            var d = await CreateDraftAsync().ConfigureAwait(false);
            await Client.Drafts.StartCheckDraftAsync(Account.Id, d.Id).ConfigureAwait(false);
            var taskId = (await Client.Drafts.GetDraftTasks(Account.Id, d.Id).ConfigureAwait(false)).ApiTaskPageItems[0].Id;
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.GetDssSignTask(Guid.Empty, d.Id, taskId).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Drafts.GetDssSignTask(Account.Id, Guid.Empty, taskId).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Drafts.GetDssSignTask(Account.Id, d.Id, Guid.Empty).ConfigureAwait(false));
            await Client.Drafts.DeleteDraftAsync(Account.Id, d.Id).ConfigureAwait(false);
        }

        [Test]
        public async Task GetDraftTask_WithValidParameters()
        {
            var d = await CreateDraftAsync().ConfigureAwait(false);
            await Client.Drafts.StartCheckDraftAsync(Account.Id, d.Id).ConfigureAwait(false);
            var taskId = (await Client.Drafts.GetDraftTasks(Account.Id, d.Id).ConfigureAwait(false)).ApiTaskPageItems[0].Id;
            Assert.DoesNotThrowAsync(async () => await Client.Drafts.GetDssSignTask(Account.Id, d.Id, taskId).ConfigureAwait(false));
            await Client.Drafts.DeleteDraftAsync(Account.Id, d.Id).ConfigureAwait(false);
        }

        private async Task<Draft> CreateDraftAsync() => await Client.Drafts.CreateDraftAsync(Account.Id, validDraftMetaRequest).ConfigureAwait(false);

        private async Task DeleteDraftAsync(Draft d) => await Client.Drafts.DeleteDraftAsync(Account.Id, d.Id).ConfigureAwait(false);

        private async Task<DraftDocument> CreateFilledDocument() =>
            await CreateFilledDocument(draft).ConfigureAwait(false);

        private async Task<DraftDocument> CreateFilledDocument(Draft d) =>
            await Client.Drafts.CreateDocumentAsync(
                Account.Id,
                d.Id,
                new DocumentRequest
                {
                    //Base64Content = Convert.ToBase64String(new byte[] {1}),
                    Description = new DocumentDescriptionRequest
                    {
                        ContentType = "application/json",
                        Filename = "Filename",
                        Type = new Urn("nid", "nss")
                    },
                    Signature = validDraftMetaRequest.Sender.Certificate.PublicKey
                }).ConfigureAwait(false);
    }
}