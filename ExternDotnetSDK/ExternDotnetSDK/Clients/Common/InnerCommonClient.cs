using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces.Logging;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Clients.Common
{
    /// <summary>
    ///     Class for sending requests, getting responses and logging exceptions
    /// </summary>
    internal class InnerCommonClient
    {
        protected readonly ILogger Logger;
        protected readonly IRequestFactory RequestFactory;
        protected readonly IRequestSender RequestSender;

        public InnerCommonClient(ILogger logger, IRequestSender requestSender, IRequestFactory requestFactory)
        {
            Logger = logger;
            RequestSender = requestSender;
            RequestFactory = requestFactory;
        }

        public async Task<TResult> SendRequestAsync<TResult>(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object contentDto = null)
        {
            var request = RequestFactory.CreateAuthorizedRequest(method, uriPath, uriQueryParams, contentDto);
            return JsonConvert.DeserializeObject<TResult>(await TryGetResponseAsync(request));
        }

        public async Task SendRequestAsync(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object contentDto = null)
        {
            var request = RequestFactory.CreateAuthorizedRequest(method, uriPath, uriQueryParams, contentDto);
            await TryGetResponseAsync(request);
        }

        private async Task<string> TryGetResponseAsync(IHaveHttpRequestMessage request)
        {
            var response = await RequestSender.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Logger.Log(response, e);
                throw;
            }
        }
    }
}