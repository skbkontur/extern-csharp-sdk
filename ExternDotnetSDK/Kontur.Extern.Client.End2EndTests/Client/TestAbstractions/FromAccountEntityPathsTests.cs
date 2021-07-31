using System.Linq;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Accounts;
using Kontur.Extern.Client.ApiLevel.Models.Certificates;
using Kontur.Extern.Client.End2EndTests.Client.TestContext;
using Kontur.Extern.Client.End2EndTests.TestHelpers;
using Kontur.Extern.Client.Model.Numbers;
using Kontur.Extern.Client.Testing.Lifetimes;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client.TestAbstractions
{
    public abstract class FromAccountEntityPathsTests : IAsyncLifetime
    {
        private readonly ITestOutputHelper output;
        private readonly Lifetime lifetime = null!;
        internal readonly KonturExternTestContext Context;

        protected FromAccountEntityPathsTests(ITestOutputHelper output)
        {
            this.output = output;
            Context = new KonturExternTestContext(output);
            // NOTE: suppress compiler warning -- it will be initialized during InitializeAsync
            Account = null!;
            AccountCertificate = null!;
        }

        protected Account Account { get; private set; }
        protected CertificateDto AccountCertificate { get; private set; }

        public async Task InitializeAsync()
        {
            var accountScope = lifetime.Add(await Context.Accounts.CreateAccount(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org"));
            Account = accountScope.Entity;
            AccountCertificate = (await Context.Accounts.GetAccountCertificatesAsync(Account.Id)).First();
            
            output.PrintSeparator("INITIALIZED");
        }

        public async Task DisposeAsync()
        {
            output.PrintSeparator("DISPOSING");
            await lifetime.DisposeAsync();
        }
    }
}