using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KeApiClientOpenSdk.Clients.Authentication;
using KeApiClientOpenSdk.Clients.Common.ResponseMessages;

namespace KeApiClientOpenSdk.Clients.Common.RequestSenders
{
    public interface IRequestSender
    {
        IAuthenticationProvider AuthenticationProvider { get; }
        string ApiKey { get; }

        Task<IResponseMessage> SendAsync(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object content = null,
            TimeSpan? timeout = null);
    }
}