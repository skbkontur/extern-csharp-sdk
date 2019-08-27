using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KeApiOpenSdk.Clients.Authentication;
using KeApiOpenSdk.Clients.Common.ResponseMessages;

namespace KeApiOpenSdk.Clients.Common.RequestSenders
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