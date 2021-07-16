using System;
using Kontur.Extern.Client.HttpLevel.Options;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Vostok.Clusterclient.Core;
using Vostok.Logging.Abstractions;
using Request = Vostok.Clusterclient.Core.Model.Request;

namespace Kontur.Extern.Client.HttpLevel.ClusterClientAdapters
{
    internal class HttpRequestsFactory : IHttpRequestsFactory
    {
        private readonly RequestSendingOptions options;
        private readonly AuthenticationOptions authOptions;
        private readonly IClusterClient clusterClient;
        private readonly IJsonSerializer serializer;
        private readonly ILog log;

        public HttpRequestsFactory(
            RequestSendingOptions options, 
            AuthenticationOptions authOptions,
            IClusterClient clusterClient,
            IJsonSerializer serializer,
            ILog log)
        {
            this.options = options;
            this.authOptions = authOptions;
            this.clusterClient = clusterClient;
            this.serializer = serializer;
            this.log = log;
        }
        
        public IHttpRequest Get(Uri url) => CreateHttpRequest(Request.Get(url));

        public IPayloadHttpRequest Put(Uri url) => CreateHttpRequest(Request.Put(url));

        public IPayloadHttpRequest Post(Uri url) => CreateHttpRequest(Request.Post(url));

        public IHttpRequest Delete(Uri url) => CreateHttpRequest(Request.Delete(url));

        private HttpRequest CreateHttpRequest(Request request) => new(request, options, authOptions, clusterClient, serializer, log);
    }
}