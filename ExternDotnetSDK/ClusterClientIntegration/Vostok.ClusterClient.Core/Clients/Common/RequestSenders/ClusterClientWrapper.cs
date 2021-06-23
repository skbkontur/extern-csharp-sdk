using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Authentication.Providers;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Clients.Common.ResponseMessages;
using Kontur.Extern.Client.Vostok.Vostok.ClusterClient.Core.Clients.Common.ResponseMessages;
using Newtonsoft.Json;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Request = Kontur.Extern.Client.Clients.Common.Requests.Request;

namespace Kontur.Extern.Client.Vostok.Vostok.ClusterClient.Core.Clients.Common.RequestSenders
{
    //public class ClusterClientWrapper : IRequestSender
    //{
    //    private readonly IClusterClient client;

    //    public ClusterClientWrapper(IAuthenticationProvider authenticationProvider, string apiKey, IClusterClient client)
    //    {
    //        AuthenticationProvider = authenticationProvider;
    //        ApiKey = apiKey;
    //        this.client = client;
    //    }

    //    public IAuthenticationProvider AuthenticationProvider { get; }
    //    public string ApiKey { get; }

    //    public Task<IResponseMessage> SendJsonAsync(Client.Clients.Common.Requests.Request request, TimeSpan? timeout = null)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<IResponseMessage> SendAsync(
    //        HttpMethod method,
    //        string uriPath,
    //        Dictionary<string, object> uriQueryParams = null,
    //        object content = null,
    //        TimeSpan? timeout = null)
    //    {
    //        var request = await CreateRequest(method, uriPath, uriQueryParams, content, timeout).ConfigureAwait(false);
    //        var response = await client.SendAsync(request, timeout: timeout).ConfigureAwait(false);
    //        return new ClusterResultWrapper(response);
    //    }

    //    //todo перейти на внутренний request 
    //    private async Task<Request> CreateRequest(
    //        HttpMethod method,
    //        string uriPath,
    //        Dictionary<string, object> uriQueryParams,
    //        object content,
    //        TimeSpan? timeout)
    //    {
    //        await AuthenticationProvider.AuthenticateAsync();
    //        var request = new Request(
    //                method.ToString().ToUpperInvariant(),
    //                GetFullUri(uriPath, uriQueryParams),
    //                new Content(Convert.FromBase64String(JsonConvert.SerializeObject(content))))
    //            //.WithAuthorizationHeader("Bearer", AuthenticationProvider.CurrentResponse.AccessToken)
    //            //не работает т.к. требуется внутренний request
    //            .WithHeader(SenderConstants.ApiKeyHeader, ApiKey)
    //            .WithContentTypeHeader(SenderConstants.MediaType);
    //        return timeout != null ? request.WithHeader(SenderConstants.TimeoutHeader, timeout.Value.ToString("c")) : request;
    //    }

    //    private static Uri GetFullUri(string uriPath, Dictionary<string, object> uriQueryParams)
    //    {
    //        var urlBuilder = new RequestUrlBuilder(uriPath);
    //        if (uriQueryParams != null)
    //            foreach (var queryParam in uriQueryParams)
    //                urlBuilder.AppendToQuery(queryParam.Key, queryParam.Value);
    //        return urlBuilder.Build();
    //    }
    //}
}