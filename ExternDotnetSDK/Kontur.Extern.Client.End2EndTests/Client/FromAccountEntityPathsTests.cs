using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Accounts;
using Kontur.Extern.Client.End2EndTests.Client.TestContext;
using Kontur.Extern.Client.Model.Numbers;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client
{
    public abstract class FromAccountEntityPathsTests : IAsyncLifetime
    {
        private readonly ITestOutputHelper output;
        internal EntityScope<Account> AccountScope = null!;
        internal readonly KonturExternTestContext Context;

        public FromAccountEntityPathsTests(ITestOutputHelper output)
        {
            this.output = output;
            Context = new KonturExternTestContext(output);
        }

        protected Account Account => AccountScope.Entity;

        public async Task InitializeAsync()
        {
            AccountScope = await Context.Accounts.CreateAccount(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org");
            PrintSeparator("INITIALIZED");
        }

        public async Task DisposeAsync()
        {
            PrintSeparator("DISPOSING");
            await AccountScope.DisposeAsync();
        }

        private void PrintSeparator(string title)
        {
            output.WriteLine(new string('-', 10) + $" {title} " + new string('-', 10));
        }
    }
}