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
            
            var accounts = await externCtx.Accounts.List().AllAsync();
            await externCtx.Accounts.WithId(loadedAccount.Id).DeleteAsync();

            async Task PlayWithAccountRelatedEntities(IAccountContext accountContext)
            {
                var allCertificates = await accountContext.Certificates.AllAsync();
                var top10Certificates = await accountContext.Certificates.Take(10).LoadAsync();
                
                var top5Warrants = await accountContext.Warrants(5).LoadAsync();
                var secondPageOfWarrants = await accountContext.Warrants().Paging().LoadPageAsync(1);
            }
        }

        public static async Task PlayWithOrganizations(IAccountContext accountCtx)
        {
            var organizationsCtx = accountCtx.Organizations;
            var createdOrganization = await organizationsCtx.CreateAsync("inn", "kpp", "name");

            var orgCtx = organizationsCtx.WithId(createdOrganization.Id);
            var loadedOrganization = await orgCtx.GetAsync();

            var topTenOrganizations = await organizationsCtx.List().Take(10).LoadAsync();
            var allOrganizationsWithParticularInn = await organizationsCtx.List("some inn").AllAsync();
            
            await orgCtx.DeleteAsync();
        }

        public static async Task PlayWithDocflowDeferredMethods(IAccountContext accountCtx)
        {
            var docflowId = Guid.NewGuid();
            var documentId = Guid.NewGuid();
            var documentCtx = accountCtx.Docflows.WithId(docflowId).Documents.WithId(documentId);
            
            var decrypting = await documentCtx.StartDssDecryptAsync();
            await decrypting.WaitForCompletion();
            // or
            var decryptStatus = await documentCtx.GetDssDecryptStatusAsync(decrypting.TaskId);
        }
    }
}