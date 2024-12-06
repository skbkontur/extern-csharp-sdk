using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.Documents;
using Microsoft.AspNetCore.JsonPatch;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestContext
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
        public Task<IReadOnlyList<IDocflow>> ListByFilter(Guid accountId, DocflowFilterBuilder filter) =>
            konturExtern.Accounts.WithId(accountId).Docflows.List(filter).SliceBy(100).LoadAllAsync();

        public Task<IDocflowWithDocuments?> GetDocflowOrNull(Guid accountId, Guid docflowId) => 
            konturExtern.Accounts.WithId(accountId).Docflows.WithId(docflowId).TryGetAsync();

        public Task<IDocflowWithDocuments> PatchDocflow(Guid accountId, Guid docflowId, JsonPatchDocument<IDocflowWithDocuments> patch) =>
            konturExtern.Accounts.WithId(accountId).Docflows.WithId(docflowId).PatchAsync(patch);

        public Task<Document> PatchDocument(Guid accountId, Guid docflowId, Guid documentId, JsonPatchDocument<Document> patch) =>
            konturExtern.Accounts.WithId(accountId).Docflows.WithId(docflowId).Documents.WithId(documentId).PatchAsync(patch);

        public Task<IDocflowWithDocuments> GetDocflow(Guid accountId, Guid docflowId) => 
            konturExtern.Accounts.WithId(accountId).Docflows.WithId(docflowId).GetAsync();
    }
}