using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.End2EndTests.Client.TestAuthProvider;
using Kontur.Extern.Client.End2EndTests.TestClusterClient;
using Kontur.Extern.Client.End2EndTests.TestLogging;
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

        public KonturExternTestContext(ILog log)
        {
            this.log = log;
            var clusterClient = ClusterClientFactory.CreateTestClient("https://extern-api.testkontur.ru/", log);
            konturExtern = ExternFactory
                .WithClusterClient(clusterClient, log)
                .WithTestOpenIdAuthProvider()
                .Create();
        }

        public IExtern Extern => konturExtern;

        public AccountTestContext Accounts => new(konturExtern, CreateScope);
        public OrganizationTestContext Organizations => new(konturExtern, CreateScope);

        private ValueTask<EntityScope<TEntity>> CreateScope<TEntity>(
            Func<Task<TEntity>> entityCreate,
            Func<TEntity, Task> entityDelete)
        {
            return EntityScope<TEntity>.Create(entityCreate, entityDelete, log);
        }
    }
}