using Kontur.Extern.Client.ApiLevel;
using Kontur.Extern.Client.HttpLevel;

namespace Kontur.Extern.Client.Common
{
    public interface IExternClientServices
    {
        IHttpRequestsFactory Http { get; }
        IKeApiClient Api { get; }
    }
}