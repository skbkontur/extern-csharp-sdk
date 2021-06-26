using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Organizations;

namespace Kontur.Extern.Client.Concept
{
    internal static class AccountContextExtensions
    {
        public static async Task SecretAccountMethodAsync(this IAccountContext accountCtx, string parameter) => 
            await accountCtx.MixinClassStyleAsync(new SecretAccountMethodExtension(parameter));

        private class SecretAccountMethodExtension : IExtension<AccountPath, Unit>
        {
            private readonly string parameter;

            public SecretAccountMethodExtension(string parameter) => this.parameter = parameter;

            public async Task<Unit> Execute(IKeApiClient client, AccountPath contextPath, Options options)
            {
                await client.Accounts.DeleteAccountAsync(contextPath.AccountId, options.DefaultWriteTimeout);
                return default;
            }
        }
    }
    
    internal static class OrganizationContextExtensions
    {
        public static async Task<Organization> UpdateOrganizationAsync(this IOrganizationContext organizationContext, string inn, string kpp, string name) =>
            await organizationContext.MixinDelegateStyleAsync(
                (client, path, options) => client.Organizations.UpdateOrganizationAsync(
                    path.AccountId,
                    path.OrganizationId,
                    /*inn, kpp,*/
                    name,
                    options.DefaultWriteTimeout)
            );
    }
}