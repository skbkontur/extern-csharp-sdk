using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Drafts;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta;
using Kontur.Extern.Client.Model.Drafts;

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
    }
}