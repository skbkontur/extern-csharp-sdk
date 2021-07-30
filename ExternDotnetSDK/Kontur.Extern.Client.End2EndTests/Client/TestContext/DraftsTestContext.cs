using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Drafts;
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

        public Task<Draft?> GetDraftOrNull(Guid accountId, Guid docflowId) => 
            konturExtern.Accounts.WithId(accountId).Drafts.WithId(docflowId).TryGetAsync();

        public Task<Draft> GetDraft(Guid accountId, Guid docflowId) => 
            konturExtern.Accounts.WithId(accountId).Drafts.WithId(docflowId).GetAsync();

        public ValueTask<EntityScope<Draft>> CreateNew(Guid accountId, NewDraft newDraft) =>
            scopeFactory(
                () => konturExtern.Accounts.WithId(accountId).Drafts.CreateDraftAsync(newDraft),
                draft => konturExtern.Accounts.WithId(accountId).Drafts.WithId(draft.Id).DeleteAsync()
            );
    }
}