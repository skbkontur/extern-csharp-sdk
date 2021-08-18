using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.Models;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool;
using Kontur.Extern.Client.Testing.End2End.Environment;
using Kontur.Extern.Client.Testing.Fakes.Logging;
using Kontur.Extern.Client.Testing.Lifetimes;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment
{
    public class IsolatedAccountEnvironment : IAsyncLifetime
    {
        private readonly AuthTestData authTestData;
        private readonly ExternTestTool externTestTool;
        private GeneratedAccount generatedAccount = null!;
        private readonly Lifetime lifetime;

        [UsedImplicitly]
        public IsolatedAccountEnvironment(IMessageSink messageSink)
        {
            authTestData = AuthTestData.LoadFromJsonFile();
            IResponseCache responseCache = authTestData.TestDataGenerateLevel switch
            {
                TestDataGenerationLevel.TestRun => new EmptyCache(),
                TestDataGenerationLevel.TempFolder => JsonFileResponseCache.InTempFolder(),
                TestDataGenerationLevel.CurrentDirectory => JsonFileResponseCache.InCurrentFolder(),
                _ => throw new ArgumentOutOfRangeException(nameof(authTestData.TestDataGenerateLevel), authTestData.TestDataGenerateLevel, null)
            };
            var log = new TestLog(messageSink);
            lifetime = new Lifetime(log);
            externTestTool = new ExternTestTool(authTestData.ApiKey, responseCache, lifetime, log);
        }

        internal ExternTestTool ExternTestTool => externTestTool;
        internal GeneratedAccount GeneratedAccount => generatedAccount;
        public AuthTestData TestData => authTestData;

        public async Task InitializeAsync() => 
            generatedAccount = await externTestTool.GenerateLegalEntityAccountAsync("the_org", GeneratedAccount.DefaultChiefName);

        public async Task DisposeAsync() => await lifetime.DisposeAsync();
    }
}