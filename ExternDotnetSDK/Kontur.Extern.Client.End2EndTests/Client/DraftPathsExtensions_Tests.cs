using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Model.Configuration;
using Kontur.Extern.Client.Model.Documents;
using Kontur.Extern.Client.Model.Documents.Contents;
using Kontur.Extern.Client.Model.Drafts;
using Kontur.Extern.Client.Model.Numbers;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Drafts.Documents;
using Kontur.Extern.Client.Models.Drafts.Meta;
using Kontur.Extern.Client.Testing.Assertions;
using Kontur.Extern.Client.Testing.ExternTestTool.Models.Requests;
using Kontur.Extern.Client.Testing.Generators;
using Kontur.Extern.Client.Testing.Helpers;
using Xunit;
using Xunit.Abstractions;
using DraftDocument = Kontur.Extern.Client.Model.Drafts.DraftDocument;
using Sender = Kontur.Extern.Client.Models.Drafts.Meta.Sender;
using Signature = Kontur.Extern.Client.Model.Signature;

namespace Kontur.Extern.Client.End2EndTests.Client
{
    public class DraftPathsExtensions_Tests : GeneratedAccountTests
    {
        private readonly Randomizer randomizer = new();
        private readonly AuthoritiesCodesGenerator codesGenerator = new();

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
                .OfType(DocumentType.Fns.Fns534.Report);

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
                .OfType(DocumentType.Fns.Fns534.Report);

            var addedDocumentId = await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);

            addedDocumentId.Should().Be(documentId);
        }
        
        [Fact]
        public async Task Should_fail_when_getting_not_exists_document()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;

            Func<Task> func = async () => await Context.Drafts.GetDocument(AccountId, createdDraft.Id, Guid.NewGuid());

            func.Should().Throw<ApiException>().And.Message.Should().Contain("NotFound");
        }
        
        [Fact]
        public async Task Should_get_a_created_document()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;
            
            var document = DraftDocument
                .WithNewId(new StreamDocumentContent(new MemoryStream(new byte[]{1, 2, 3}), "application/pdf"))
                .OfType(DocumentType.Fns.Fns534.Report)
                .WithSignature(GeneratedAccount.CertificatePublicPart);
            var addedDocumentId = await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);

            var expectedDocument = new Models.Drafts.Documents.DraftDocument
            {
                Id = addedDocumentId,
                Description = new DocumentDescription
                {
                    ContentType = "application/pdf",
                    Type = DocumentType.Fns.Fns534.Report.ToUrn(),
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

            draftDocument.Should().BeEquivalentTo(expectedDocument, c => c.Excluding(x => x.SignatureContentLink).Excluding(x => x.Contents).Excluding(x => x.Signatures));
            draftDocument.Signatures.Should().HaveCount(1);
        }
        
        [Fact]
        public async Task Should_add_a_signature_to_a_drafts_document()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;

            var document = DraftDocument
                .WithNewId(new StreamDocumentContent(new MemoryStream(new byte[]{1, 2, 3}), "application/pdf"))
                .OfType(DocumentType.Fns.Fns534.Report);
            var addedDocumentId = await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);
            
            var addedSignatureId = await Context.Drafts.AddSignature(AccountId, createdDraft.Id, addedDocumentId, GeneratedAccount.CertificatePublicPart);
            addedSignatureId.Should().NotBeEmpty();
            
            await DocumentShouldContainSignatureWithId(addedSignatureId);

            async Task DocumentShouldContainSignatureWithId(Guid signatureId)
            {
                var draftDocument = await Context.Drafts.GetDocument(AccountId, createdDraft.Id, addedDocumentId);
                draftDocument.Signatures.Should().ContainSingle(s => s.Id == signatureId);
            }
        }
        
        [Fact]
        public async Task Should_download_a_signature_of_a_drafts_document()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;

            Signature documentSignature = new byte[] {1, 2, 3};
            var document = DraftDocument
                .WithNewId(new StreamDocumentContent(new MemoryStream(new byte[]{1, 2, 3}), "application/pdf"))
                .OfType(DocumentType.Fns.Fns534.Report);
            var addedDocumentId = await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);
            var signatureId = await Context.Drafts.AddSignature(AccountId, createdDraft.Id, addedDocumentId, documentSignature);

            var signature = await Context.Drafts.GetSignature(AccountId, createdDraft.Id, document.DocumentId, signatureId);

            signature.ToBytes().Should().BeEquivalentTo(documentSignature.ToBytes());
        }

        [Fact]
        public async Task Should_download_a_content_of_a_document()
        {
            var context = Context.OverrideExternOptions(x => x.OverrideContentsOptions(new ContentManagementOptions(downloadChunkSize: 80*1024)));
            
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;

            var contentBytes = randomizer.Bytes(500*1024);
            var document = DraftDocument
                .WithNewId(new StreamDocumentContent(new MemoryStream(contentBytes)))
                .OfType(DocumentType.Fns.Fns534.Report);
            var addedDocumentId = await context.Drafts.SetDocument(AccountId, createdDraft.Id, document);
            
            var draftDocument = await context.Drafts.GetDocument(AccountId, createdDraft.Id, addedDocumentId);

            var contentId = draftDocument.Contents.Single().ContentId;
            await using var stream = await context.Contents.GetContentStream(AccountId, contentId);
            
            var allBytes = await stream.ReadAllBytesAsync();

            allBytes.ShouldHaveExpectedBytes(contentBytes);
        }

        [Fact]
        public async Task Should_remove_a_document_from_a_draft()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;
            
            var document = DraftDocument
                .WithNewId(new StreamDocumentContent(new MemoryStream(new byte[]{1, 2, 3}), "application/pdf"))
                .OfType(DocumentType.Fns.Fns534.Report);
            var documentId = await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);

            await Context.Drafts.DeleteDocument(AccountId, createdDraft.Id, documentId);

            Func<Task> func = async () => await Context.Drafts.GetDocument(AccountId, createdDraft.Id, documentId);

            func.Should().Throw<ApiException>().And.Message.Should().Contain("NotFound");
        }

        [Fact]
        public async Task Should_check_a_correct_draft()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;

            var fufSschContent = await GenerateCorrectFufSschContent();
            var document = DraftDocument
                .WithNewId(new ByteDocumentContent(fufSschContent, "application/xml"));
            await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);

            var checkingStatus = await Context.Drafts.CheckDraft(AccountId, createdDraft.Id);

            checkingStatus.IsSuccessful.Should().BeTrue();

            async Task<byte[]> GenerateCorrectFufSschContent()
            {
                var inn = GeneratedAccount.Inn.ToString();
                var kpp = GeneratedAccount.Kpp.ToString();
                var orgName = GeneratedAccount.OrganizationName;// "the_org";
                var (surname, firstName, patronymicName) = GeneratedAccount.ChiefName;
                return await ExternTestTool.GenerateFufSschFileContentAsync(
                    generateCertificateIfAbsentInSender: true,
                    sender: new Testing.ExternTestTool.Models.Requests.Sender(inn, Kpp: kpp, OrgName: orgName),
                    payer: new Payer(inn, orgName, kpp, new PersonFullName(surname, firstName, patronymicName))
                );
            }
        }

        [Fact]
        public async Task Should_check_an_incorrect_draft_withing_document_failures()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;

            var fufSschContent = await GenerateInCorrectFufSschContent();
            var document = DraftDocument
                .WithNewId(new ByteDocumentContent(fufSschContent, "application/xml"))
                .WithFileName("invalid.xml");
            await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);

            var checkingStatus = await Context.Drafts.CheckDraft(AccountId, createdDraft.Id);

            checkingStatus.IsSuccessful.Should().BeFalse();

            async Task<byte[]> GenerateInCorrectFufSschContent()
            {
                var inn = GeneratedAccount.Inn.ToString();
                return await ExternTestTool.GenerateFufSschFileContentAsync(
                    new Testing.ExternTestTool.Models.Requests.Sender(inn),
                    generateCertificateIfAbsentInSender: true
                );
            }
        }
        
        [Fact]
        public async Task Should_send_a_correct_draft_without_content()
        {
            var fssRegNumber = codesGenerator.FssRegNumber();
            var newDraft = CreateDraftOfDefaultAccount(DraftRecipient.Fss(FssCode.Parse("12341")), fssRegNumber);
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;

            var document = DraftDocument
                .FssSedoProviderSubscriptionSubscribeRequestForRegistrationNumber(Guid.NewGuid());
            await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);

            var docflow = await Context.Drafts.SendDraftOrFail(AccountId, createdDraft.Id);

            docflow.Should().NotBeNull();
        }
        
        [Fact]
        public async Task Should_fail_if_send_an_incorrect_draft()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;

            var fufSschContent = await GenerateIncorrectFufSschContent();
            var document = DraftDocument
                .WithNewId(new ByteDocumentContent(fufSschContent, "application/xml"))
                .WithFileName("invalid.xml")
                .OfType(DocumentType.Fns.Fns534.Report);
            await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);

            Func<Task> func = async () => await Context.Drafts.SendDraftOrFail(AccountId, createdDraft.Id);

            var exception = func.Should().Throw<ApiException>().Which;
            exception.Message.Should().Contain(document.DocumentId.ToString()).And.Contain(createdDraft.Id.ToString());
            
            output.WriteLine("Thrown error:");
            output.WriteLine(exception.ToString());

            async Task<byte[]> GenerateIncorrectFufSschContent()
            {
                var inn = GeneratedAccount.Inn.ToString();
                return await ExternTestTool.GenerateFufSschFileContentAsync(
                    new Testing.ExternTestTool.Models.Requests.Sender(inn),
                    generateCertificateIfAbsentInSender: true
                );
            }
        }
        
        [Fact]
        public async Task Should_return_error_if_trying_to_send_an_incorrect_draft()
        {
            var newDraft = CreateDraftOfDefaultAccount();
            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);
            var createdDraft = entityScope.Entity;

            var fufSschContent = await GenerateIncorrectFufSschContent();
            var document = DraftDocument
                .WithNewId(new ByteDocumentContent(fufSschContent, "application/xml"))
                .WithFileName("invalid.xml")
                .OfType(DocumentType.Fns.Fns534.Report);
            await Context.Drafts.SetDocument(AccountId, createdDraft.Id, document);

            var result = await Context.Drafts.TrySendDraft(AccountId, createdDraft.Id);

            result.TryGetFailureResult(out var sendingFailure).Should().BeTrue();
            sendingFailure.CheckStatus.Should().NotBeNull();

            async Task<byte[]> GenerateIncorrectFufSschContent()
            {
                var inn = GeneratedAccount.Inn.ToString();
                return await ExternTestTool.GenerateFufSschFileContentAsync(
                    new Testing.ExternTestTool.Models.Requests.Sender(inn),
                    generateCertificateIfAbsentInSender: true
                );
            }
        }
        
        private DraftMetadata CreateDraftOfDefaultAccount(DraftRecipient? recipient = null, FssRegNumber? fssRegNumber = null)
        {
            var certInn = GeneratedAccount.Inn;
            var certKpp = GeneratedAccount.Kpp;
            var senderCert = GeneratedAccount.CertificatePublicPart;

            var payer = DraftPayer.LegalEntityPayer(certInn, certKpp);
            if (fssRegNumber != null)
            {
                payer.WithFssRegNumber(fssRegNumber);
            }

            return new DraftMetadata(
                payer,
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

            RelatedDocument? relatedDocumentRequest = null;
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
                Certificate = new CertificatePublicKey
                {
                    Content = Encoding.UTF8.GetString(sender.Certificate.PublicKey)
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