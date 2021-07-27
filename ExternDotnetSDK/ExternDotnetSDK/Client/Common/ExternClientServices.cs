using Kontur.Extern.Client.ApiLevel;
using Kontur.Extern.Client.Auth.Abstractions;
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
            IAuthenticationProvider authProvider)
        {
            Http = http;
            Api = api;
            LongOperationsPollingStrategy = longOperationsPollingStrategy;
            AuthProvider = authProvider;
        }
        
        public IHttpRequestsFactory Http { get; }
        public IKeApiClient Api { get; }
        public IPollingStrategy LongOperationsPollingStrategy { get; }
        
        public IAuthenticationProvider AuthProvider { get; }
    }
}