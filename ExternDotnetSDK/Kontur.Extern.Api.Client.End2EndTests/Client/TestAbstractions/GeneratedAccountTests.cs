using System;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestContext;
using Kontur.Extern.Api.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Api.Client.Testing.ExternTestTool;
using Kontur.Extern.Api.Client.Testing.ExternTestTool.Models.Results;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestAbstractions
{
    [Collection(IsolatedAccountEnvironmentCollection.Name)]
    public abstract class GeneratedAccountTests 
    {
        private readonly IsolatedAccountEnvironment environment;
        protected readonly ITestOutputHelper output;

        protected GeneratedAccountTests(ITestOutputHelper output, IsolatedAccountEnvironment environment)
        {
            this.environment = environment;
            this.output = output;
            Context = new KonturExternTestContext(output, environment.TestData, environment.GeneratedAccount.Credentials);
        }

        internal KonturExternTestContext Context { get; }

        protected private ExternTestTool ExternTestTool => environment.ExternTestTool.WithLoggingTo(output);
        protected Guid AccountId => environment.GeneratedAccount.AccountId;
        internal GeneratedAccount GeneratedAccount => environment.GeneratedAccount;
    }
}