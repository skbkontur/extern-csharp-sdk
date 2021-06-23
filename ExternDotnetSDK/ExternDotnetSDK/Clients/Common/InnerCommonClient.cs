using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Common.ResponseMessages;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Clients.Common
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
        public async Task<TResult> SendJsonRequestAsync<TResult>(Request request, TimeSpan? timeout = null)
        {
            var response = await RequestSender.SendJsonAsync(request, timeout).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResult>(await response.TryGetResponseAsync(Logger).ConfigureAwait(false));
        }

        public async Task SendJsonRequestAsync(Request request, TimeSpan? timeout = null)
        {
            var response = await RequestSender.SendJsonAsync(request, timeout).ConfigureAwait(false);
            await response.TryGetResponseAsync(Logger).ConfigureAwait(false);
        }

        public async Task<TResult> SendJsonRequestAsync<TResult>(Request request, TimeSpan? timeout = null)
        {
            var response = await RequestSender.SendJsonAsync(request, timeout).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResult>(await response.TryGetResponseAsync(Logger).ConfigureAwait(false));
        }

        public async Task SendJsonRequestAsync(Request request, TimeSpan? timeout = null)
        {
            var response = await RequestSender.SendJsonAsync(request, timeout).ConfigureAwait(false);
            await response.TryGetResponseAsync(Logger).ConfigureAwait(false);
        }

        public async Task<TResult> SendRequestAsync<TResult>(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object contentDto = null,
            TimeSpan? timeout = null)
        {
            var response = await RequestSender.SendAsync(method, uriPath, uriQueryParams, contentDto, timeout).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResult>(await response.TryGetResponseAsync(Logger).ConfigureAwait(false));
        }

        public async Task SendRequestAsync(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object contentDto = null,
            TimeSpan? timeout = null)
        {
            var response = await RequestSender.SendAsync(method, uriPath, uriQueryParams, contentDto, timeout).ConfigureAwait(false);
            await response.TryGetResponseAsync(Logger).ConfigureAwait(false);
        }
    }
}