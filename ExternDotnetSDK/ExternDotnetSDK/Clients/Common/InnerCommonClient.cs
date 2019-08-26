using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common.Logging;
using ExternDotnetSDK.Clients.Common.RequestSenders;
using ExternDotnetSDK.Clients.Common.ResponseMessages;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Clients.Common
{
    internal class InnerCommonClient
    {
        protected readonly ILogger Logger;
        protected readonly IRequestSender RequestSender;

        public InnerCommonClient(ILogger logger, IRequestSender requestSender)
        {
            Logger = logger;
            RequestSender = requestSender;
        }

        public async Task<TResult> SendRequestAsync<TResult>(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object contentDto = null,
            TimeSpan? timeout = null)
        {
            var response = await RequestSender.SendAsync(method, uriPath, uriQueryParams, contentDto, timeout);
            return JsonConvert.DeserializeObject<TResult>(await TryGetResponseAsync(response));
        }

        public async Task SendRequestAsync(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object contentDto = null,
            TimeSpan? timeout = null)
        {
            var response = await RequestSender.SendAsync(method, uriPath, uriQueryParams, contentDto, timeout);
            await TryGetResponseAsync(response);
        }

        private async Task<string> TryGetResponseAsync(IResponseMessage response)
        {
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