using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Authentication.Client.Extensions;
using Kontur.Extern.Client.Clients.Authentication.Client.Factories;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.RequestMessages;

namespace Kontur.Extern.Client.Clients.Authentication.Client
{
    class Sender
    {
        //Есть желание заменить на IRequestSender но, мне не хватает там возможности добавлять хэдеры
        private readonly HttpClient client;
        private readonly ILogger log;

        public Sender(HttpClient client, ILogger log)
        {
            this.client = client;
            this.log = log;
        }

        public Task<ServiceResult<TError>> SendAsync<TError>(HttpRequestMessage request,  TimeSpan? timeout)
            where TError : class
        {
            return SendAsync(request,  timeout, ServiceResultFactory<TError>.Instance);
        }


        public Task<ServiceResult<TResponse, TError>> SendAsync<TResponse, TError>(HttpRequestMessage request,  TimeSpan? timeout)
            where TResponse : class
            where TError : class
        {
            return SendAsync(request,  timeout, ServiceResultFactory<TResponse, TError>.Instance);
        }

        private async Task<TResult> SendAsync<TResult>(
            HttpRequestMessage request,
            TimeSpan? timeout,
            IResultFactory<TResult> resultFactory)
            where TResult : ServiceResult
        {
            request.TrySetTimeoutHeader(timeout);
            var clusterResult = await client.SendAsync(request)
                .ConfigureAwait(false);

            switch (clusterResult.StatusCode)
            {
                case HttpStatusCode.OK:
                    var response = clusterResult;
                    return resultFactory.CreateSuccessful(response);

                case HttpStatusCode.InternalServerError:
                    LogUnavailable(clusterResult);
                    return resultFactory.CreateServiceUnavailable(clusterResult.ReasonPhrase);

                default:
                    LogUnknown(clusterResult);
                    return resultFactory.CreateUnknownError(clusterResult.ReasonPhrase);
            }
        }

        private void LogUnknown(HttpResponseMessage result) => log.Log($"HTTP request terminated with unknown status '{result.ReasonPhrase}'.");

        private void LogUnavailable(HttpResponseMessage result) => log.Log($"Service unavailable with status '{result.ReasonPhrase}'.");
    }
}
