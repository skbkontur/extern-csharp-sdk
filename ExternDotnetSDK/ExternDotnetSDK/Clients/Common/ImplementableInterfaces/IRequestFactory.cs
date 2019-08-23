using System.Collections.Generic;
using System.Net.Http;

namespace ExternDotnetSDK.Clients.Common.ImplementableInterfaces
{
    public interface IRequestFactory
    {
        IAuthenticationProvider AuthenticationProvider { get; }

        /// <summary>
        ///     Use IAuthenticationProvider in this method to sign user in the request
        /// </summary>
        IHaveHttpRequestMessage CreateAuthorizedRequest(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object content = null);
    }
}