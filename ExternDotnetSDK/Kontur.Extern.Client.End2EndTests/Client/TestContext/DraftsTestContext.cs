using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Model;
using Kontur.Extern.Client.Model.Drafts;
using Kontur.Extern.Client.Model.Drafts.LongOperationStatuses;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Drafts;
using Kontur.Extern.Client.Models.Drafts.Meta;
using Kontur.Extern.Client.Primitives.LongOperations;

namespace Kontur.Extern.Client.End2EndTests.Client.TestContext
{
    internal class DraftsTestContext
    {
        private readonly IExtern konturExtern;
        private readonly EntityScopeFactory<Draft> scopeFactory;

        public DraftsTestContext(IExtern konturExtern, EntityScopeFactory<Draft> scopeFactory)
        {
            this.konturExtern = konturExtern;
            this.scopeFactory = scopeFactory;
        }

        public Task<Draft?> GetDraftOrNull(Guid accountId, Guid draftId) => 
            konturExtern.Accounts.WithId(accountId).Drafts.WithId(draftId).TryGetAsync();

        public Task<Draft> GetDraft(Guid accountId, Guid draftId) => 
            konturExtern.Accounts.WithId(accountId).Drafts.WithId(draftId).GetAsync();

        public ValueTask<EntityScope<Draft>> CreateNew(Guid accountId, DraftMetadata draftMetadata) =>
            scopeFactory(
                () => konturExtern.Accounts.WithId(accountId).Drafts.CreateDraftAsync(draftMetadata),
                draft => konturExtern.Accounts.WithId(accountId).Drafts.WithId(draft.Id).DeleteAsync()
            );
        
        public Task<DraftMeta> UpdateDraftMetadata(Guid accountId, Guid draftId, DraftMetadata metadata) => 
            konturExtern.Accounts.WithId(accountId).Drafts.WithId(draftId).UpdateMetadataAsync(metadata);

        public Task<Guid> SetDocument(Guid accountId, Guid draftId, IDraftDocument document) => 
            konturExtern.Accounts.WithId(accountId).Drafts.WithId(draftId).SetDocumentAsync(document);
        
        public Task DeleteDocument(Guid accountId, Guid draftId, Guid documentId) => 
            konturExtern.Accounts.WithId(accountId).Drafts.WithId(draftId).Document(documentId).DeleteAsync();

        public Task<Models.Drafts.Documents.DraftDocument> GetDocument(Guid accountId, Guid draftId, Guid documentId) =>
            konturExtern.Accounts.WithId(accountId).Drafts.WithId(draftId).Document(documentId).GetAsync();
        
        public async Task<Signature> GetSignature(Guid accountId, Guid draftId, Guid documentId, Guid signatureId) =>
            await konturExtern.Accounts.WithId(accountId).Drafts.WithId(draftId).Document(documentId).Signature(signatureId).DownloadAsync().ConfigureAwait(false);

        public Task<Guid> AddSignature(Guid accountId, Guid draftId, Guid documentId, Signature signature) =>
            konturExtern.Accounts.WithId(accountId).Drafts.WithId(draftId)
                .Document(documentId)
                .AddSignatureAsync(signature.ToBase64String());
        
        public async Task<DraftCheckingStatus> CheckDraft(Guid accountId, Guid draftId)
        {
            var awaiter = await konturExtern.Accounts.WithId(accountId).Drafts.WithId(draftId).Check().StartAsync();
            return await awaiter.WaitForCompletion();
        }
        
        public async Task<LongOperationResult<Docflow, DraftSendingFailure>> TrySendDraft(Guid accountId, Guid draftId)
        {
            var awaiter = await konturExtern.Accounts.WithId(accountId).Drafts.WithId(draftId).TrySend().StartAsync();
            return await awaiter.WaitForSuccessOrFailure();
        }
        
        public async Task<Docflow> SendDraftOrFail(Guid accountId, Guid draftId)
        {
            var awaiter = await konturExtern.Accounts.WithId(accountId).Drafts.WithId(draftId).Send().StartAsync();
            return await awaiter.WaitForCompletion();
        }
    }
}