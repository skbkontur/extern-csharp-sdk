using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Concept2
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    internal class UsageSamples
    {
        private readonly IExternContextFactory externCtxFactory;

        public UsageSamples(IExternContextFactory externCtxFactory) => this.externCtxFactory = externCtxFactory;

        public IExternContext CreateContext(IExternCredentials credentials) => externCtxFactory.Create(credentials);

        public async Task PlayWithAccounts(IExternContext externCtx)
        {
            var createdAccount = await externCtx.Accounts.CreateAsync("inn", "kpp", "org");

            var loadedAccount = await externCtx.Accounts.WithId(createdAccount.Id).GetAsync();

            await PlayWithAccountRelatedEntities(externCtx.Accounts.WithId(loadedAccount.Id));

            await PlayWithOrganizations(externCtx.Accounts.WithId(loadedAccount.Id));
            
            var accounts = await externCtx.Accounts.List().SliceBy(100).Skip(10).LoadSliceAsync();
            await externCtx.Accounts.WithId(loadedAccount.Id).DeleteAsync();

            async Task PlayWithAccountRelatedEntities(AccountPath accountPath)
            {
                var allCertificates = await accountPath.Certificates().SliceBy(10).LoadAllAsync();
                var top10Certificates = await accountPath.Certificates().SliceBy(10).LoadSliceAsync();
                
                var top5Warrants = await accountPath.Warrants().SliceBy(5).LoadSliceAsync();
                var secondPageOfWarrants = await accountPath.Warrants().Paging(5).LoadPageAsync(1);
            }
        }

        public static async Task PlayWithOrganizations(AccountPath accountPath)
        {
            var organizationsPath = accountPath.Organizations;
            var createdOrganization = await organizationsPath.CreateAsync("inn", "kpp", "name");

            var orgPath = organizationsPath.WithId(createdOrganization.Id);
            var loadedOrganization = await orgPath.GetAsync();

            var topTenOrganizations = await organizationsPath.List().SliceBy(10).LoadSliceAsync();
            var allOrganizationsWithParticularInn = await organizationsPath.List("some inn").SliceBy(5).LoadAllAsync();
            
            await orgPath.DeleteAsync();
        }

        public static async Task PlayWithDocflowDeferredMethods(AccountPath accountPath)
        {
            var docflowId = Guid.NewGuid();
            var documentId = Guid.NewGuid();
            var documentPath = accountPath.Docflows.WithId(docflowId).Documents.WithId(documentId);

            var decrypting = await documentPath.DssDecrypt().StartAsync();
            await decrypting.WaitForCompletion();
            // or
            var decryptStatus = await documentPath.DssDecrypt().CheckStatusAsync(decrypting.TaskId);
            // or
            Guid restoredTaskId;
            await documentPath.DssDecrypt().ContinueAwait(restoredTaskId).WaitForCompletion();
        }
    }
}