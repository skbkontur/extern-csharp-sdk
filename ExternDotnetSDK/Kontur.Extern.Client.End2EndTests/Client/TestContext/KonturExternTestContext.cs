using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.End2EndTests.Client.TestAuthProvider;
using Kontur.Extern.Client.Testing.End2End.ClusterClient;
using Kontur.Extern.Client.Testing.Fakes.Logging;
using Vostok.Logging.Abstractions;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client.TestContext
{
    internal class KonturExternTestContext
    {
        private readonly ILog log;
        private readonly IExtern konturExtern;

        public KonturExternTestContext(ITestOutputHelper output)
            : this(new TestLog(output))
        {
        }

        public KonturExternTestContext(ILog log, bool tryResolveUnauthorizedResponsesAutomatically = true)
        {
            this.log = log;
            var clusterClient = ClusterClientFactory.CreateTestClient("https://extern-api.testkontur.ru/", log);
            var externBuilder = ExternBuilder
                .WithClusterClient(clusterClient, log)
                .WithTestOpenIdAuthProvider();

            if (tryResolveUnauthorizedResponsesAutomatically)
            {
                externBuilder.TryResolveUnauthorizedResponsesAutomatically();
            }

            konturExtern = externBuilder.Create();
        }

        public IExtern Extern => konturExtern;

        public AccountTestContext Accounts => new(konturExtern, CreateScope);
        public OrganizationTestContext Organizations => new(konturExtern, CreateScope);
        public DocflowsTestContext Docflows => new(konturExtern, CreateScope);
        public DraftsTestContext Drafts => new(konturExtern, CreateScope);

        private ValueTask<EntityScope<TEntity>> CreateScope<TEntity>(
            Func<Task<TEntity>> entityCreate,
            Func<TEntity, Task> entityDelete)
        {
            return EntityScope<TEntity>.Create(entityCreate, entityDelete, log);
        }
    }
}