using Kontur.Extern.Client.ApiLevel;
using Kontur.Extern.Client.Auth.Abstractions;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Http;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Primitives.Polling;
using Kontur.Extern.Client.Uploading;

namespace Kontur.Extern.Client.Common
{
    public interface IExternClientServices
    {
        IHttpRequestsFactory Http { get; }
        IJsonSerializer JsonSerializer { get; }
        IKeApiClient Api { get; }
        IPollingStrategy LongOperationsPollingStrategy { get; }
        IAuthenticationProvider AuthProvider { get; }
        ICrypt Crypt { get; }
        IContentService ContentService { get; }
    }
}