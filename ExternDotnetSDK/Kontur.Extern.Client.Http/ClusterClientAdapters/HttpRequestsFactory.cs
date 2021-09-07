#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Http.Options;
using Kontur.Extern.Client.Http.Serialization;
using Vostok.Clusterclient.Core;
using Vostok.Logging.Abstractions;
using Request = Vostok.Clusterclient.Core.Model.Request;

namespace Kontur.Extern.Client.Http.ClusterClientAdapters
{
    public class HttpRequestsFactory : IHttpRequestsFactory
    {
        private readonly RequestTimeouts requestTimeouts;
        private readonly Func<Request, TimeSpan, Task<Request>>? requestTransformAsync;
        private readonly Func<IHttpResponse, ValueTask<bool>>? errorResponseHandler;
        private readonly FailoverAsync? failover;
        private readonly IClusterClient clusterClient;
        private readonly IJsonSerializer serializer;
        
        public HttpRequestsFactory(
            RequestTimeouts requestTimeouts, 
            Func<Request, TimeSpan, Task<Request>>? requestTransformAsync,
            Func<IHttpResponse, ValueTask<bool>>? errorResponseHandler,
            FailoverAsync? failover,
            IClusterClientFactory clusterClientFactory,
            IJsonSerializer serializer,
            ILog log)
        {
            if (clusterClientFactory is null)
                throw new ArgumentNullException(nameof(clusterClientFactory));

            this.requestTimeouts = requestTimeouts ?? throw new ArgumentNullException(nameof(requestTimeouts));
            this.requestTransformAsync = requestTransformAsync;
            this.errorResponseHandler = errorResponseHandler;
            this.failover = failover;
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            
            clusterClient = clusterClientFactory.Create(log ?? throw new ArgumentNullException(nameof(log)));
        }
        
        public IHttpRequest Get(Uri url) => CreateHttpRequest(Request.Get(url));

        public IPayloadHttpRequest Put(Uri url) => CreateHttpRequest(Request.Put(url));

        public IPayloadHttpRequest Post(Uri url) => CreateHttpRequest(Request.Post(url));

        public IHttpRequest Delete(Uri url) => CreateHttpRequest(Request.Delete(url));

        private HttpRequest CreateHttpRequest(Request request) => 
            new(request, requestTimeouts, requestTransformAsync, errorResponseHandler, failover, clusterClient, serializer);
    }
}