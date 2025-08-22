using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Primitives.Polling;
using Kontur.Extern.Api.Client.Uploading;
using Kontur.Extern.Api.Client.Auth.Abstractions;
using Kontur.Extern.Api.Client.Cryptography;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Http.Serialization;

namespace Kontur.Extern.Api.Client.Common
{
    [PublicAPI]
    public interface IExternClientServices
    {
        IHttpRequestFactory Http { get; }
        IJsonSerializer JsonSerializer { get; }
        IExternHttpClient Api { get; }
        IPollingStrategy LongOperationsPollingStrategy { get; }
        IAuthenticator Authenticator { get; }
        ICrypt Crypt { get; }
        IContentService ContentService { get; }
    }
}