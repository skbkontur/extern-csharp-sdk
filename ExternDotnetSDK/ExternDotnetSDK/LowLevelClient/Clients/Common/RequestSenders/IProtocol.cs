#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Exceptions;
using Kontur.Extern.Client.Models.Accounts;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Time;
using RequestUrlBuilder = Vostok.Clusterclient.Core.Model.RequestUrlBuilder;

namespace Kontur.Extern.Client.Clients.Common.RequestSenders
{
    using CCRequest=Vostok.Clusterclient.Core.Model.Request;

    class ProtocolExample
    {
        public async Task Send(IProtocol protocol)
        {
            // get list
            var accountsUrl = new RequestUrlBuilder("v1")
                .AppendToQuery("skip", 1)
                .AppendToQuery("take", 10)
                .Build();
            var response = await protocol.Get(accountsUrl).SendAsync();
            var accountList = response.GetMessage<AccountList>();

            // delete
            var accountId = accountList.Accounts.First().Id;
            await protocol.Delete(new Uri($"v1/{accountId}")).SendAsync();
            
            // create
            var requestDto = new CreateAccountRequestDto();
            var createAccountResponse = await protocol.Post(new Uri("v1"))
                .WithPayload(requestDto)
                .SendAsync();
            var createdAccount = createAccountResponse.GetMessage<Account>();
        }
    } 
    
    interface IProtocol
    {
        IRequest Get(Uri url);
        IPayloadRequest Put(Uri url);
        IPayloadRequest Post(Uri url);
        IRequest Delete(Uri url);
    }

    internal interface IPayloadRequest : IRequest
    {
        IRequest WithPayload<TRequestMessage>(TRequestMessage message);
    }

    internal interface IRequest
    {
        Task<IResponse> SendAsync(TimeSpan? timeout = null);
    }

    internal interface IResponse
    {
        TResponseMessage GetMessage<TResponseMessage>();
    }

    internal class Protocol : IProtocol, IPayloadRequest
    {
        private CCRequest? request; 
        private readonly RequestSendingOptions options;
        private readonly AuthenticationOptions authOption;
        private readonly IClusterClient clusterClient;
        private readonly IRequestBodySerializer serializer;

        public Protocol(
            RequestSendingOptions options, 
            AuthenticationOptions authOption,
            IClusterClient clusterClient,
            IRequestBodySerializer serializer)
        {
            this.options = options;
            this.authOption = authOption;
            this.clusterClient = clusterClient;
            this.serializer = serializer;
        }

        public IRequest Get(Uri url)
        {
            request = CCRequest.Get(url);
            return this;
        }

        public IPayloadRequest Put(Uri url)
        {
            request = CCRequest.Put(url);
            return this;
        }

        public IPayloadRequest Post(Uri url)
        {
            request = CCRequest.Post(url);
            return this;
        }

        public IRequest Delete(Uri url)
        {
            request = CCRequest.Delete(url);
            return this;
        }

        IRequest IPayloadRequest.WithPayload<TRequestMessage>(TRequestMessage message)
        {
            var memoryStream = new MemoryStream();
            serializer.SerializeToJsonStream(message, memoryStream);
            memoryStream.Position = 0;
            Request.WithContent(new StreamContent(memoryStream));
            return this;
        }

        async Task<IResponse> IRequest.SendAsync(TimeSpan? timeout)
        {
            timeout ??= Request.IsWriteRequest() ? options.DefaultWriteTimeout : options.DefaultReadTimeout;
            var timeBudget = TimeBudget.StartNew(timeout.Value);

            var sessionId = await authOption.Provider.GetSessionId(timeBudget.Remaining).ConfigureAwait(false);

            var leftTimeout = timeBudget.Remaining;
            var resultRequest = BuildRequest(Request, sessionId, leftTimeout);

            var result = await clusterClient.SendAsync(resultRequest, leftTimeout).ConfigureAwait(false);
            return new ProtocolResponse(resultRequest, result.Response.EnsureSuccessStatusCode(), serializer);
        }

        private CCRequest BuildRequest(CCRequest request, string sessionId, TimeSpan? timeout)
        {
            var resultRequest = request
                .WithAuthorizationHeader(SenderConstants.AuthSidHeader, sessionId)
                .WithHeader(SenderConstants.ApiKeyHeader, authOption.ApiKey);

            return timeout == null 
                ? resultRequest 
                : resultRequest.WithHeader(SenderConstants.TimeoutHeader, timeout.Value.ToString("c"));
        }

        private CCRequest Request => request!;
    }

    internal class ProtocolResponse : IResponse
    {
        private readonly CCRequest request;
        private readonly Response response;
        private readonly IRequestBodySerializer serializer;

        public ProtocolResponse(CCRequest request, Response response, IRequestBodySerializer serializer)
        {
            this.request = request;
            this.response = response;
            this.serializer = serializer;
        }
        
        public TResponseMessage GetMessage<TResponseMessage>()
        {
            if (!response.HasStream)
                throw Errors.ResponseHasToHaveBody(request.ToString(true, false));
            if (response.Headers.ContentType != SenderConstants.MediaType) 
                throw Errors.ResponseHasUnexpectedContentType(request.ToString(true, false), response, SenderConstants.MediaType);

            var memoryStream = response.Content.ToMemoryStream();
            return serializer.DeserializeFromJson<TResponseMessage>(memoryStream);
        }
    }

    internal static class CCRequestExtension
    {
        public static bool IsWriteRequest(this CCRequest request) => !request.Method.Equals(RequestMethods.Get, StringComparison.OrdinalIgnoreCase);
    }
}