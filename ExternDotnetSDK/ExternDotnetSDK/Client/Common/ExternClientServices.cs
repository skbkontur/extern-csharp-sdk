using Kontur.Extern.Client.ApiLevel;
using Kontur.Extern.Client.Auth.Abstractions;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Http;
using Kontur.Extern.Client.Model.Configuration;
using Kontur.Extern.Client.Primitives.Polling;
using Kontur.Extern.Client.Uploading;

namespace Kontur.Extern.Client.Common
{
    internal class ExternClientServices : IExternClientServices
    {
        public ExternClientServices(
            ContentManagementOptions contentManagementOptions,
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
            ContentService = new ContentService(api.Contents, contentManagementOptions);
        }
        
        public IHttpRequestsFactory Http { get; }
        public IKeApiClient Api { get; }
        public IPollingStrategy LongOperationsPollingStrategy { get; }
        
        public IAuthenticationProvider AuthProvider { get; }
        public ICrypt Crypt { get; }
        public IContentService ContentService { get; }
    }
}