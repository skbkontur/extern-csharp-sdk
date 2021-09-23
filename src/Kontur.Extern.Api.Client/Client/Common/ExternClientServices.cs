using System;
using Kontur.Extern.Api.Client.ApiLevel;
using Kontur.Extern.Api.Client.Model.Configuration;
using Kontur.Extern.Api.Client.Primitives.Polling;
using Kontur.Extern.Api.Client.Uploading;
using Kontur.Extern.Api.Client.Auth.Abstractions;
using Kontur.Extern.Api.Client.Cryptography;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Http.Serialization;

namespace Kontur.Extern.Api.Client.Common
{
    internal class ExternClientServices : IExternClientServices
    {
        public ExternClientServices(
            ContentManagementOptions contentManagementOptions,
            IHttpRequestsFactory http,
            IJsonSerializer jsonSerializer,
            IKeApiClient api,
            IPollingStrategy longOperationsPollingStrategy,
            IAuthenticator authenticator,
            ICrypt crypt)
        {
            Http = http ?? throw new ArgumentNullException(nameof(http));
            JsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
            Api = api ?? throw new ArgumentNullException(nameof(api));
            LongOperationsPollingStrategy = longOperationsPollingStrategy ?? throw new ArgumentNullException(nameof(longOperationsPollingStrategy));
            Authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
            Crypt = crypt ?? throw new ArgumentNullException(nameof(crypt));
            ContentService = new ContentService(api.Contents, contentManagementOptions);
        }
        
        public IHttpRequestsFactory Http { get; }
        public IJsonSerializer JsonSerializer { get; }
        public IKeApiClient Api { get; }
        public IPollingStrategy LongOperationsPollingStrategy { get; }
        
        public IAuthenticator Authenticator { get; }
        public ICrypt Crypt { get; }
        public IContentService ContentService { get; }
    }
}