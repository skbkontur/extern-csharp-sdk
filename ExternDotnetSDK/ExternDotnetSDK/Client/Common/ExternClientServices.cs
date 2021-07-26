using Kontur.Extern.Client.ApiLevel;
using Kontur.Extern.Client.Http;
using Kontur.Extern.Client.Primitives.Polling;

namespace Kontur.Extern.Client.Common
{
    internal class ExternClientServices : IExternClientServices
    {
        public ExternClientServices(IHttpRequestsFactory http, IKeApiClient api, IPollingStrategy longOperationsPollingStrategy)
        {
            Http = http;
            Api = api;
            LongOperationsPollingStrategy = longOperationsPollingStrategy;
        }
        
        public IHttpRequestsFactory Http { get; }
        public IKeApiClient Api { get; }
        public IPollingStrategy LongOperationsPollingStrategy { get; }
    }
}