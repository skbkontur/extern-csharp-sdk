using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KeApiOpenSdk.Clients.Authentication;
using KeApiOpenSdk.Clients.Common.RequestSenders;
using KeApiOpenSdk.Clients.Common.ResponseMessages;
using KonturInfrastructureIntegration.Vostok.ClusterClient.Core.Clients.Common.ResponseMessages;
using Newtonsoft.Json;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;

namespace KonturInfrastructureIntegration.Vostok.ClusterClient.Core.Clients.Common.RequestSenders
{
    public class ClusterClientWrapper : IRequestSender
    {
        private readonly IClusterClient client;

        public ClusterClientWrapper(IAuthenticationProvider authenticationProvider, string apiKey, IClusterClient client)
        {
            AuthenticationProvider = authenticationProvider;
            ApiKey = apiKey;
            this.client = client;
        }

        public IAuthenticationProvider AuthenticationProvider { get; }
        public string ApiKey { get; }

        public async Task<IResponseMessage> SendAsync(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object content = null,
            TimeSpan? timeout = null)
        {
            var request = await CreateRequest(method, uriPath, uriQueryParams, content, timeout);
            var response = await client.SendAsync(request, timeout: timeout);
            return new ClusterResultWrapper(response);
        }

        private async Task<Request> CreateRequest(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams,
            object content,
            TimeSpan? timeout)
        {
            var request = new Request(method.ToString().ToUpperInvariant(), GetFullUri(uriPath, uriQueryParams))
                .WithAuthorizationHeader(SenderConstants.AuthSidHeader, await AuthenticationProvider.GetSessionId())
                .WithHeader(SenderConstants.ApiKeyHeader, ApiKey);
            if (content != null)
            {
                request = request.WithContent(Convert.FromBase64String(JsonConvert.SerializeObject(content)))
                    .WithContentTypeHeader(SenderConstants.MediaType);
            }
            if (timeout != null)
                request = request.WithHeader(SenderConstants.TimeoutHeader, timeout.Value.ToString("c"));
            return request;
        }

        private static Uri GetFullUri(string uriPath, Dictionary<string, object> uriQueryParams)
        {
            var urlBuilder = new RequestUrlBuilder(uriPath);
            if (uriQueryParams != null)
            {
                foreach (var queryParam in uriQueryParams)
                    urlBuilder.AppendToQuery(queryParam.Key, queryParam.Value);
            }
            return urlBuilder.Build();
        }
    }
}