using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Docflows;

namespace Kontur.Extern.Client.End2EndTests.Client.TestContext
{
    internal class DocflowsTestContext
    {
        private readonly IExtern konturExtern;
        private readonly EntityScopeFactory<IDocflowWithDocuments> scopeFactory;

        public DocflowsTestContext(IExtern konturExtern, EntityScopeFactory<IDocflowWithDocuments> scopeFactory)
        {
            this.konturExtern = konturExtern;
            this.scopeFactory = scopeFactory;
        }

        public Task<IReadOnlyList<IDocflow>> ListAll(Guid accountId) => 
            konturExtern.Accounts.WithId(accountId).Docflows.List().SliceBy(100).LoadAllAsync();

        public Task<IDocflowWithDocuments?> GetDocflowOrNull(Guid accountId, Guid docflowId) => 
            konturExtern.Accounts.WithId(accountId).Docflows.WithId(docflowId).TryGetAsync();

        public Task<IDocflowWithDocuments> GetDocflow(Guid accountId, Guid docflowId) => 
            konturExtern.Accounts.WithId(accountId).Docflows.WithId(docflowId).GetAsync();
    }
}