using System.Linq;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Accounts;
using Kontur.Extern.Client.End2EndTests.Client.TestContext;
using Kontur.Extern.Client.End2EndTests.TestHelpers;
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
        }

        protected Account DefaultAccount { get; private set; }

        public async Task InitializeAsync()
        {
            DefaultAccount = (await Context.Accounts.LoadAllAccountsAsync()).Single();
            output.PrintSeparator("INITIALIZED");
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}