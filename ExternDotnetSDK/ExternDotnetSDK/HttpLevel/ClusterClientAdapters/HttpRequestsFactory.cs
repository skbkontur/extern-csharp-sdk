#nullable enable
using System;
using System.Threading.Tasks;
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
        private readonly Func<Request, TimeSpan, Task<Request>>? requestTransformAsync;
        private readonly Func<IHttpResponse, bool>? errorResponseHandler;
        private readonly IClusterClient clusterClient;
        private readonly IJsonSerializer serializer;
        private readonly ILog log;

        public HttpRequestsFactory(
            RequestSendingOptions options,
            IClusterClient clusterClient,
            IJsonSerializer serializer,
            ILog log)
            : this(options, null, clusterClient, serializer, log)
        {
        }

        public HttpRequestsFactory(
            RequestSendingOptions options,
            Func<Request, TimeSpan, Task<Request>>? requestTransformAsync,
            IClusterClient clusterClient,
            IJsonSerializer serializer,
            ILog log)
            : this(options, requestTransformAsync, null, clusterClient, serializer, log)
        {
        }

        public HttpRequestsFactory(
            RequestSendingOptions options, 
            Func<Request, TimeSpan, Task<Request>>? requestTransformAsync,
            Func<IHttpResponse, bool>? errorResponseHandler,
            IClusterClient clusterClient,
            IJsonSerializer serializer,
            ILog log)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.requestTransformAsync = requestTransformAsync;
            this.errorResponseHandler = errorResponseHandler;
            this.clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.log = log ?? throw new ArgumentNullException(nameof(log));;
        }
        
        public IHttpRequest Get(Uri url) => CreateHttpRequest(Request.Get(url));

        public IPayloadHttpRequest Put(Uri url) => CreateHttpRequest(Request.Put(url));

        public IPayloadHttpRequest Post(Uri url) => CreateHttpRequest(Request.Post(url));

        public IHttpRequest Delete(Uri url) => CreateHttpRequest(Request.Delete(url));

        private HttpRequest CreateHttpRequest(Request request) => new(request, options, requestTransformAsync, errorResponseHandler, clusterClient, serializer, log);
    }
}