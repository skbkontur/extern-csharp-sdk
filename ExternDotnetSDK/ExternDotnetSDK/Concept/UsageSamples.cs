using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Concept
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

            await PlayWithExtensions(externCtx.Accounts.WithId(loadedAccount.Id));
            
            var accounts = await externCtx.Accounts.List().SliceBy(100).Skip(10).LoadSliceAsync();
            await externCtx.Accounts.WithId(loadedAccount.Id).DeleteAsync();

            async Task PlayWithAccountRelatedEntities(IAccountContext accountContext)
            {
                var allCertificates = await accountContext.Certificates.SliceBy(10).LoadAllAsync();
                var top10Certificates = await accountContext.Certificates.SliceBy(10).LoadSliceAsync();
                
                var top5Warrants = await accountContext.Warrants.SliceBy(5).LoadSliceAsync();
                var secondPageOfWarrants = await accountContext.Warrants.Paging(5).LoadPageAsync(1);
            }
        }

        public static async Task PlayWithOrganizations(IAccountContext accountCtx)
        {
            var organizationsCtx = accountCtx.Organizations;
            var createdOrganization = await organizationsCtx.CreateAsync("inn", "kpp", "name");

            var orgCtx = organizationsCtx.WithId(createdOrganization.Id);
            var loadedOrganization = await orgCtx.GetAsync();

            var topTenOrganizations = await organizationsCtx.List().SliceBy(10).LoadSliceAsync();
            var allOrganizationsWithParticularInn = await organizationsCtx.List("some inn").SliceBy(5).LoadAllAsync();
            
            await orgCtx.DeleteAsync();
        }


        public static async Task PlayWithExtensions(IAccountContext accountCtx)
        {
            await accountCtx.SecretAccountMethodAsync("secret");

            await accountCtx.Organizations.WithId(Guid.NewGuid()).UpdateOrganizationAsync("inn", "kpp", "name");
        }

        public static async Task PlayWithDocflowDeferredMethods(IAccountContext accountCtx)
        {
            var docflowId = Guid.NewGuid();
            var documentId = Guid.NewGuid();
            var documentCtx = accountCtx.Docflows.WithId(docflowId).Documents.WithId(documentId);

            var decrypting = await documentCtx.DssDecrypt.StartAsync();
            await decrypting.WaitForCompletion();
            // or
            var decryptStatus = await documentCtx.DssDecrypt.CheckStatusAsync(decrypting.TaskId);
            // or
            Guid restoredTaskId;
            documentCtx.DssDecrypt.ContinueAwait(restoredTaskId).WaitForCompletion();
        }
    }
}