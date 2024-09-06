#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Http.Configurations;
using Kontur.Extern.Api.Client.Http.Options;
using Kontur.Extern.Api.Client.Http.Serialization;
using Vostok.Clusterclient.Core;
using Vostok.Logging.Abstractions;
using Request = Vostok.Clusterclient.Core.Model.Request;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters
{
    public class HttpRequestFactory : IHttpRequestFactory
    {
        private readonly RequestTimeouts requestTimeouts;
        private readonly Func<Request, TimeSpan, Task<Request>>? requestTransformAsync;
        private readonly Func<IHttpResponse, ValueTask<bool>>? errorResponseHandler;
        private readonly FailoverAsync? failover;
        private readonly IClusterClient clusterClient;
        private readonly IJsonSerializer serializer;
        private readonly ILog log;

        public HttpRequestFactory(
            IHttpClientConfiguration configuration,
            RequestTimeouts requestTimeouts,
            Func<Request, TimeSpan, Task<Request>>? requestTransformAsync,
            Func<IHttpResponse, ValueTask<bool>>? errorResponseHandler,
            FailoverAsync? failover,
            IJsonSerializer serializer,
            ILog log)
        {
            this.requestTimeouts = requestTimeouts ?? throw new ArgumentNullException(nameof(requestTimeouts));
            this.requestTransformAsync = requestTransformAsync;
            this.errorResponseHandler = errorResponseHandler;
            this.failover = failover;
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.log = log;

            clusterClient = ClusterClientFactory.CreateClusterClient(configuration, log);
        }

        public IHttpRequest Get(Uri url) => CreateHttpRequest(Request.Get(url));

        public IPayloadHttpRequest Put(Uri url) => CreateHttpRequest(Request.Put(url));

        public IPayloadHttpRequest Post(Uri url) => CreateHttpRequest(Request.Post(url));

        public IHttpRequest Delete(Uri url) => CreateHttpRequest(Request.Delete(url));

        public IPayloadHttpRequest Verb(string method, Uri url) => CreateHttpRequest(new Request(method, url));

        private HttpRequest CreateHttpRequest(Request request) =>
            new(request, requestTimeouts, requestTransformAsync, errorResponseHandler, failover, clusterClient, serializer, log);
    }
}