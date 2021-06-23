using System;
using Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests;
using Kontur.Extern.Client.Clients.Authentication.Providers;
using Kontur.Extern.Client.Clients.Authentication.TokenAuth.Kontur.Extern.Client.Clients.Authentication;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Cryptography;

namespace Kontur.Extern.Client.Engine.EngineBuilder
{
    public class EngineBuilder : IConfiguredEngine, IAuthenticatedEngine, IFullEngine, ILoggedEngine
    {
        private readonly ClientConfiguration clientConfiguration;
        private IAuthenticationProvider authenticationProvider;
        private ILogger log;
        private IAccountIdProvider accountIdProvider;
        private readonly ICrypt cryptoProvider;
        private TimeSpan? defaultTimeout;

        private EngineBuilder(ClientConfiguration config)
        {
            clientConfiguration = config;
            cryptoProvider = new WinApiCrypt();
        }

        public static IConfiguredEngine WithConfiguration(ClientConfiguration configuration)
        {
            return new EngineBuilder(configuration);
        }

        public ILoggedEngine WithLog(ILogger log)
        {
            this.log = log;
            return this;
        }

        public IAuthenticatedEngine WithAuth(IAuthenticationProvider provider)
        {
            authenticationProvider = provider;
            return this;
        }

        public IAuthenticatedEngine WithPasswordAuth(string authenticationBaseAddress, PasswordTokenRequest passwordTokenRequest)
        {
            authenticationProvider = new OpenIdPasswordAuthenticationProvider(authenticationBaseAddress, passwordTokenRequest, log);
            return this;
        }

        public IAuthenticatedEngine WithCertificateAuth(string authenticationBaseAddress, CertificateAuthenticationRequest certificateAuthenticationRequest)
        {
            authenticationProvider = new OpenIdCertificateAuthenticationProvider(authenticationBaseAddress, certificateAuthenticationRequest, cryptoProvider, log);

            return this;
        }

        public IFullEngine WithDefaultTimeout(TimeSpan? timeout)
        {
            this.defaultTimeout = timeout;
            return this;
        }

        public ExternEngine Build()
        {
            return new ExternEngine(authenticationProvider, cryptoProvider, accountIdProvider, defaultTimeout, clientConfiguration);
        }

        public IAuthenticatedEngine WithAccountIdProvider(IAccountIdProvider idProvider)
        {
            this.accountIdProvider = idProvider;
            return this;
        }

        public IAuthenticatedEngine WithAccountId(Guid accountId)
        {
            accountIdProvider = new AccountIdProvider() {AccountId = accountId};
            return this;
        }
    }
}