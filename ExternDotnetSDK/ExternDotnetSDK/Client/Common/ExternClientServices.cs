using Kontur.Extern.Client.ApiLevel;
using Kontur.Extern.Client.Auth.Abstractions;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Http;
using Kontur.Extern.Client.Primitives.Polling;

namespace Kontur.Extern.Client.Common
{
    internal class ExternClientServices : IExternClientServices
    {
        public ExternClientServices(
            IHttpRequestsFactory http, 
            IKeApiClient api, 
            IPollingStrategy longOperationsPollingStrategy, 
            IAuthenticationProvider authProvider,
            ICrypt crypt)
        {
            Http = http;
            Api = api;
            LongOperationsPollingStrategy = longOperationsPollingStrategy;
            AuthProvider = authProvider;
            Crypt = crypt;
        }
        
        public IHttpRequestsFactory Http { get; }
        public IKeApiClient Api { get; }
        public IPollingStrategy LongOperationsPollingStrategy { get; }
        
        public IAuthenticationProvider AuthProvider { get; }
        public ICrypt Crypt { get; }
    }
}