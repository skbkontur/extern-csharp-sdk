using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Models.Drafts;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Model.Documents;
using Kontur.Extern.Client.Model.Documents.Contents;
using Kontur.Extern.Client.Model.Drafts;
using Kontur.Extern.Client.Model.Numbers;
using Kontur.Extern.Client.Testing.Assertions;
using Kontur.Extern.Client.Testing.Generators;
using Kontur.Extern.Client.Testing.Helpers;
using Xunit;
using Xunit.Abstractions;
using DraftDocument = Kontur.Extern.Client.Model.Drafts.DraftDocument;

namespace Kontur.Extern.Client.End2EndTests.Client
{
    public class DraftPathsExtensions_Tests : GeneratedAccountTests
    {
        private Randomizer randomizer = new();
        
        public DraftPathsExtensions_Tests(ITestOutputHelper output, IsolatedAccountEnvironment environment)
            : base(output, environment)
        {
        }
        
        [Fact]
        public void Get_should_fail_when_try_to_read_non_existent_draft()
        {
            Func<Task> func = async () => await Context.Drafts.GetDraft(AccountId, Guid.NewGuid());

            func.Should().Throw<ApiException>();
        }

        [Fact]
        public async Task TryGet_should_return_null_when_try_to_read_non_existent_draft()
        {
            var draft = await Context.Drafts.GetDraftOrNull(AccountId, Guid.NewGuid());

            draft.Should().BeNull();
        }

        [Fact]
        public async Task Should_create_a_new_draft()
        {
            var newDraft = CreateDraftOfDefaultAccount();

            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);

            var createdDraft = entityScope.Entity;
            var loadedDraft = await Context.Drafts.GetDraft(AccountId, createdDraft.Id);
            
            loadedDraft.Should().BeEquivalentTo(createdDraft);
        }

        [Fact]
        public async Task Deleted_draft_should_cannot_be_loaded()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            Guid draftId;
            await using (var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft))
            {
                draftId = entityScope.Entity.Id;
            }

            var draft = await Context.Drafts.GetDraftOrNull(AccountId, draftId);
            draft.Should().BeNull();
        }

        [Fact]
        public async Task Should_update_draft_metadata()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            var updatedMetadata = CreateDraftOfDefaultAccount(DraftRecipient.Fss(FssCode.Parse("12341")));
            var expectedMetadata = ToApiModel(GeneratedAccount.OrganizationName, updatedMetadata);

            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;

            await Context.Drafts.UpdateDraftMetadata(AccountId, createdDraft.Id, updatedMetadata);

            var loadedDraft = await Context.Drafts.GetDraft(AccountId, createdDraft.Id);
            
            loadedDraft.Meta.Should().BeEquivalentTo(expectedMetadata, c => c.Excluding(x => x.Sender.Certificate));
        }

        [Fact]
        public async Task Should_add_a_document_with_stream_content_to_a_draft()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;
            var documentId = Guid.NewGuid();

            var document = DraftDocument
                .WithId(documentId, new StreamDocumentContent(new MemoryStream(new byte[]{1, 2, 3})))
                .OfType(DocumentType.Fns.Fns534Report.Report);

            var addedDocumentId = await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);

            addedDocumentId.Should().Be(documentId);
        }

        [Fact]
        public async Task Should_add_a_document_with_bytes_content_to_a_draft()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;
            var documentId = Guid.NewGuid();

            var document = DraftDocument
                .WithId(documentId, new ByteDocumentContent(new byte[]{1, 2, 3}))
                .OfType(DocumentType.Fns.Fns534Report.Report);

            var addedDocumentId = await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);

            addedDocumentId.Should().Be(documentId);
        }
        
        [Fact]
        public async Task Should_get_a_document_with_stream_content_from_a_draft()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;
            
            var document = DraftDocument
                .WithNewId(new StreamDocumentContent(new MemoryStream(new byte[]{1, 2, 3}), "application/pdf"))
                .OfType(DocumentType.Fns.Fns534Report.Report);
            var addedDocumentId = await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);

            var expectedDocument = new ApiLevel.Models.Drafts.DraftDocument
            {
                Id = addedDocumentId,
                Description = new DocumentDescription
                {
                    ContentType = "application/pdf",
                    Type = DocumentType.Fns.Fns534Report.Report.ToUrn(),
                    Properties = new Dictionary<string, string?>
                    {
                        ["Encoding"] = null,
                        ["FormName"] = null,
                        ["КНД"] = null,
                        ["CorrectionNumber"] = 0.ToString(),
                        ["IsPrintable"] = false.ToString(),
                        ["Period"] = null,
                        ["OriginalFilename"] = null,
                        ["SvdregCode"] = null
                    }
                }
            };

            var draftDocument = await Context.Drafts.GetDocument(AccountId, createdDraft.Id, addedDocumentId);

            draftDocument.Should().BeEquivalentTo(expectedDocument, c => c.Excluding(x => x.DecryptedContentLink).Excluding(x => x.Contents));
        }

        [Fact]
        public async Task Should_download_a_content_of_a_document()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;

            var contentBytes = randomizer.Bytes(500*1024);
            var document = DraftDocument
                .WithNewId(new StreamDocumentContent(new MemoryStream(contentBytes)))
                .OfType(DocumentType.Fns.Fns534Report.Report);
            var addedDocumentId = await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);
            
            var draftDocument = await Context.Drafts.GetDocument(AccountId, createdDraft.Id, addedDocumentId);

            var contentId = draftDocument.Contents.Single().ContentId;
            await using var stream = await Context.Contents.GetContentStream(AccountId, contentId);
            
            var allBytes = await stream.ReadAllBytesAsync();

            allBytes.ShouldHaveExpectedBytes(contentBytes);
        }

        private DraftMetadata CreateDraftOfDefaultAccount(DraftRecipient? recipient = null)
        {
            var certInn = GeneratedAccount.Inn;
            var certKpp = GeneratedAccount.Kpp;
            var senderCert = GeneratedAccount.CertificatePublicPart;

            return new DraftMetadata(
                DraftPayer.LegalEntityPayer(certInn, certKpp),
                DraftSender.LegalEntity(certInn, certKpp, senderCert).WithIpAddress(IPAddress.Parse("8.8.8.8")),
                recipient ?? DraftRecipient.Ifns(IfnsCode.Parse("0087"), MriCode.Parse("9660"))
            );
        }
        
        private static DraftMeta ToApiModel(string orgName, DraftMetadata metadata)
        {
            var request = metadata.ToRequest();
            AdditionalInfo? additionalInfoRequest = null;
            if (request.AdditionalInfo?.Subject != null)
            {
                additionalInfoRequest = new AdditionalInfo
                {
                    Subject = request.AdditionalInfo.Subject
                };
            }

            RelatedDocument relatedDocumentRequest = null;
            if (request.RelatedDocument != null)
            {
                relatedDocumentRequest = new RelatedDocument
                {
                    RelatedDocflowId = request.RelatedDocument.RelatedDocflowId,
                    RelatedDocumentId = request.RelatedDocument.RelatedDocumentId
                };
            }

            return new DraftMeta
            {
                Payer = PayerToApiModel(orgName, request.Payer),
                Sender = SenderToApiModel(orgName, request.Sender),
                Recipient = RecipientToApiModel(request.Recipient),
                AdditionalInfo = additionalInfoRequest,
                RelatedDocument = relatedDocumentRequest
            };

            static AccountInfo PayerToApiModel(string orgName, AccountInfoRequest payer)
            {
                var accountInfo = new AccountInfo
                {
                    Name = orgName,
                    Inn = payer.Inn,
                    RegistrationNumberFss = payer.RegistrationNumberFss,
                    RegistrationNumberPfr = payer.RegistrationNumberPfr
                };
                if (payer.Organization != null)
                {
                    accountInfo.Organization = new OrganizationInfo
                    {
                        Kpp = payer.Organization.Kpp
                    };
                }

                return accountInfo;
            }
            
            static Sender SenderToApiModel(string orgName, SenderRequest sender) => new()
            {
                Name = orgName,
                Inn = sender.Inn,
                Kpp = sender.Kpp,
                IpAddress = sender.IpAddress,
                IsRepresentative = sender.IsRepresentative,
                Certificate = new Certificate
                {
                    Content = Encoding.UTF8.GetString(sender.Certificate.Content)
                }
            };

            static RecipientInfo RecipientToApiModel(RecipientInfoRequest recipient) => new()
            {
                FssCode = recipient.FssCode,
                IfnsCode = recipient.IfnsCode,
                MriCode = recipient.MriCode,
                TogsCode = recipient.TogsCode,
                UpfrCode = recipient.UpfrCode
            };
        }
    }
}