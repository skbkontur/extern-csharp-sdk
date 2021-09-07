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

        public KonturExternTestContext(ILog log, AuthTestData authTestData, Action<IExternBuilder>? overrideOptions = null)
            : this(log, authTestData, authTestData.UserCredentials, overrideOptions)
        {
        }

        public KonturExternTestContext(ITestOutputHelper output, AuthTestData authTestData, Credentials credentials, Action<IExternBuilder>? overrideOptions = null)
            : this(new TestLog(output), authTestData, credentials, overrideOptions)
        {
        }

        public KonturExternTestContext(ILog log, AuthTestData authTestData, Credentials credentials, Action<IExternBuilder>? overrideOptions = null)
        {
            this.log = log;
            this.authTestData = authTestData;
            this.credentials = credentials;
            konturExtern = CreateExtern(overrideOptions);
        }
        
        internal KonturExternTestContext OverrideExternOptions(Action<IExternBuilder> overrideOptions) => new(log, authTestData, credentials, overrideOptions);

        public IExtern Extern => konturExtern;

        public AccountTestContext Accounts => new(konturExtern, CreateScope);
        public OrganizationTestContext Organizations => new(konturExtern, CreateScope);
        public DocflowsTestContext Docflows => new(konturExtern, CreateScope);
        public DraftsTestContext Drafts => new(konturExtern, CreateScope);
        public ContentsContext Contents => new(konturExtern);

        private ValueTask<EntityScope<TEntity>> CreateScope<TEntity>(
            Func<Task<TEntity>> entityCreate,
            Func<TEntity, Task> entityDelete)
        {
            return EntityScope<TEntity>.Create(entityCreate, entityDelete, log);
        }

        private IExtern CreateExtern(Action<IExternBuilder>? overrideOptions = null)
        {
            var clusterClient = new TestingClusterClientFactory("https://extern-api.testkontur.ru/");
            var externBuilder = ExternBuilder
                .WithClusterClient(clusterClient, log)
                .WithTestOpenIdAuthProvider(authTestData, credentials);

            externBuilder.TryResolveUnauthorizedResponsesAutomatically();
            overrideOptions?.Invoke(externBuilder);

            return externBuilder.Create();
        }
    }
}