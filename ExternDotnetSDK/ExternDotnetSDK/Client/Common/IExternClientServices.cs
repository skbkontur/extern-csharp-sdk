using Kontur.Extern.Client.ApiLevel;
using Kontur.Extern.Client.Http;
using Kontur.Extern.Client.Primitives.Polling;

namespace Kontur.Extern.Client.Common
{
    public interface IExternClientServices
    {
        IHttpRequestsFactory Http { get; }
        IKeApiClient Api { get; }
        IPollingStrategy LongOperationsPollingStrategy { get; }
    }
}