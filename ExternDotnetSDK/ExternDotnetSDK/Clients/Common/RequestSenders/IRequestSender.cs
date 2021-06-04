using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Authentication;
using Kontur.Extern.Client.Clients.Authentication.Providers;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Common.ResponseMessages;

namespace Kontur.Extern.Client.Clients.Common.RequestSenders
{
    public interface IRequestSender
    {
        IAuthenticationProvider AuthenticationProvider { get; }
        string ApiKey { get; }

        Task<IResponseMessage> SendJsonAsync(Request request, TimeSpan? timeout = null);

        Task<IResponseMessage> SendAsync(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object content = null,
            TimeSpan? timeout = null);
    }
}