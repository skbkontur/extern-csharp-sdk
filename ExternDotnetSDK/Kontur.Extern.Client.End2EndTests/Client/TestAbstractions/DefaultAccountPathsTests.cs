using System.Linq;
using System.Threading.Tasks;
using Kontur.Extern.Client.End2EndTests.Client.TestContext;
using Kontur.Extern.Client.End2EndTests.TestHelpers;
using Kontur.Extern.Client.Models.Accounts;
using Kontur.Extern.Client.Models.Certificates;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client.TestAbstractions
{
    public abstract class DefaultAccountPathsTests : IAsyncLifetime
    {
        private readonly ITestOutputHelper output;
        internal readonly KonturExternTestContext Context;

        protected DefaultAccountPathsTests(ITestOutputHelper output)
        {
            this.output = output;
            Context = new KonturExternTestContext(output);
            DefaultAccount = null!; // NOTE: suppress compiler warning -- it will be initialized during InitializeAsync
            AccountCertificate = null!;
        }

        protected Account DefaultAccount { get; private set; }
        protected CertificateDto AccountCertificate { get; private set; }

        public async Task InitializeAsync()
        {
            DefaultAccount = (await Context.Accounts.LoadAllAccountsAsync()).Single();
            AccountCertificate = (await Context.Accounts.GetAccountCertificatesAsync(DefaultAccount.Id)).First();
            output.PrintSeparator("INITIALIZED");
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}