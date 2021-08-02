using System;
using Kontur.Extern.Client.End2EndTests.Client.TestContext;
using Kontur.Extern.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.Models;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client.TestAbstractions
{
    public abstract class GeneratedAccountTests : IClassFixture<IsolatedAccountEnvironment> 
    {
        private readonly IsolatedAccountEnvironment environment;
        private readonly ITestOutputHelper output;

        protected GeneratedAccountTests(ITestOutputHelper output, IsolatedAccountEnvironment environment)
        {
            this.environment = environment;
            this.output = output;
            Context = new KonturExternTestContext(output, environment.TestData, environment.GeneratedAccount.Credentials);
        }

        internal KonturExternTestContext Context { get; }

        protected Guid AccountId => environment.GeneratedAccount.AccountId;
        internal GeneratedAccount GeneratedAccount => environment.GeneratedAccount;
    }
}