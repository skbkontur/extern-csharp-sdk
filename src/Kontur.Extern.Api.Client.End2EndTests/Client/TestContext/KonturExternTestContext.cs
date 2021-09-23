using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestAuthenticator;
using Kontur.Extern.Api.Client.Testing.End2End.ClusterClient;
using Kontur.Extern.Api.Client.Testing.End2End.Environment;
using Kontur.Extern.Api.Client.Testing.Fakes.Logging;
using Vostok.Logging.Abstractions;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestContext
{
    internal class KonturExternTestContext
    {
        private readonly ILog log;
        private readonly AuthTestData authTestData;
        private readonly Credentials credentials;
        private readonly IExtern konturExtern;

        public KonturExternTestContext(ITestOutputHelper output, AuthTestData authTestData, Credentials credentials, Action<ExternBuilder.Configured>? overrideOptions = null)
            : this(new TestLog(output), authTestData, credentials, overrideOptions)
        {
        }

        private KonturExternTestContext(ILog log, AuthTestData authTestData, Credentials credentials, Action<ExternBuilder.Configured>? overrideOptions = null)
        {
            this.log = log;
            this.authTestData = authTestData;
            this.credentials = credentials;
            konturExtern = CreateExtern(overrideOptions);
        }
        
        internal KonturExternTestContext OverrideExternOptions(Action<ExternBuilder.Configured> overrideOptions) => 
            new(log, authTestData, credentials, overrideOptions);

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

        private IExtern CreateExtern(Action<ExternBuilder.Configured>? overrideOptions = null)
        {
            var clientConfiguration = new TestingHttpClientConfiguration("https://extern-api.testkontur.ru/");
            var externBuilder = new ExternBuilder()
                .WithHttpConfiguration(clientConfiguration, log)
                .WithTestOpenIdAuthenticator(authTestData, credentials)
                .TryResolveUnauthorizedResponsesAutomatically();
            
            overrideOptions?.Invoke(externBuilder);

            return externBuilder.Create();
        }
    }
}