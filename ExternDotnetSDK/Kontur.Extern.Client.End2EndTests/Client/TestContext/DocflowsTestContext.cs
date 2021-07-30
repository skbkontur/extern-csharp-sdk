using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;

namespace Kontur.Extern.Client.End2EndTests.Client.TestContext
{
    internal class DocflowsTestContext
    {
        private readonly IExtern konturExtern;
        private readonly EntityScopeFactory<Docflow> scopeFactory;

        public DocflowsTestContext(IExtern konturExtern, EntityScopeFactory<Docflow> scopeFactory)
        {
            this.konturExtern = konturExtern;
            this.scopeFactory = scopeFactory;
        }

        public Task<IReadOnlyList<DocflowPageItem>> ListAll(Guid accountId) => 
            konturExtern.Accounts.WithId(accountId).Docflows.List().SliceBy(100).LoadAllAsync();

        public Task<Docflow?> GetDocflowOrNull(Guid accountId, Guid docflowId) => 
            konturExtern.Accounts.WithId(accountId).Docflows.WithId(docflowId).TryGetAsync();

        public Task<Docflow> GetDocflow(Guid accountId, Guid docflowId) => 
            konturExtern.Accounts.WithId(accountId).Docflows.WithId(docflowId).GetAsync();
    }
}