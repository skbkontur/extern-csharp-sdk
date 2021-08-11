using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Auth.OpenId.Provider.Models;
using Kontur.Extern.Client.End2EndTests.Client.TestAuthProvider;
using Kontur.Extern.Client.Testing.End2End.ClusterClient;
using Kontur.Extern.Client.Testing.End2End.Environment;
using Kontur.Extern.Client.Testing.Fakes.Logging;
using Vostok.Logging.Abstractions;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client.TestContext
{
    internal class KonturExternTestContext
    {
        private readonly ILog log;
        private readonly AuthTestData authTestData;
        private readonly Credentials credentials;
        private readonly IExtern konturExtern;
        
        public KonturExternTestContext(ITestOutputHelper output)
            : this(new TestLog(output), AuthTestData.LoadFromJsonFile())
        {
        }

        public KonturExternTestContext(ILog log, AuthTestData authTestData, bool tryResolveUnauthorizedResponsesAutomatically = true)
            : this(log, authTestData, authTestData.UserCredentials, tryResolveUnauthorizedResponsesAutomatically)
        {
        }

        public KonturExternTestContext(ITestOutputHelper output, AuthTestData authTestData, Credentials credentials)
            : this(new TestLog(output), authTestData, credentials)
        {
        }

        public KonturExternTestContext(ILog log, AuthTestData authTestData, Credentials credentials, bool tryResolveUnauthorizedResponsesAutomatically = true)
        {
            this.log = log;
            this.authTestData = authTestData;
            this.credentials = credentials;
            konturExtern = CreateExtern(tryResolveUnauthorizedResponsesAutomatically);
        }

        public IExtern Extern => konturExtern;

        public AccountTestContext Accounts => new(konturExtern, CreateScope);
        public OrganizationTestContext Organizations => new(konturExtern, CreateScope);
        public DocflowsTestContext Docflows => new(konturExtern, CreateScope);
        public DraftsTestContext Drafts => new(konturExtern, CreateScope);
        public ContentsContext Contents => new(konturExtern);

        public KonturExternTestContext WithoutAutoUnauthorizedResponsesResolving()
        {
            return new KonturExternTestContext(log, authTestData, credentials, false);
        }

        private ValueTask<EntityScope<TEntity>> CreateScope<TEntity>(
            Func<Task<TEntity>> entityCreate,
            Func<TEntity, Task> entityDelete)
        {
            return EntityScope<TEntity>.Create(entityCreate, entityDelete, log);
        }

        private IExtern CreateExtern(bool tryResolveUnauthorizedResponsesAutomatically)
        {
            var clusterClient = ClusterClientFactory.CreateTestClient("https://extern-api.testkontur.ru/", log);
            var externBuilder = ExternBuilder
                .WithClusterClient(clusterClient, log)
                .WithTestOpenIdAuthProvider(authTestData, credentials);

            if (tryResolveUnauthorizedResponsesAutomatically)
            {
                externBuilder.TryResolveUnauthorizedResponsesAutomatically();
            }

            return externBuilder.Create();
        }
    }
}