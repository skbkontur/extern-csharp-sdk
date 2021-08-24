#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using Kontur.Extern.Client.Auth.Abstractions;
using Kontur.Extern.Client.Authentication;
using Kontur.Extern.Client.Common;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Http;
using Kontur.Extern.Client.Http.ClusterClientAdapters;
using Kontur.Extern.Client.Http.Options;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Model.Configuration;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives.Polling;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client
{
    internal class ExternFactory
    {
        public bool EnableUnauthorizedFailover { get; set; }
        
        public IExtern Create(
            ContentManagementOptions contentManagementOptions,
            IClusterClient clusterClient, 
            IPollingStrategy pollingStrategy, 
            ICrypt cryptoProvider, 
            RequestTimeouts requestTimeouts, 
            IAuthenticationProvider authProvider, 
            IJsonSerializer jsonSerializer)
        {
            var http = CreateHttp(clusterClient, requestTimeouts, authProvider, jsonSerializer);
            var api = new KeApiClient(http);
            var services = new ExternClientServices(contentManagementOptions, http, jsonSerializer, api, pollingStrategy, authProvider, cryptoProvider);
            return new Extern(services);
        }

        private HttpRequestsFactory CreateHttp(IClusterClient clusterClient, RequestTimeouts requestTimeouts, IAuthenticationProvider authenticationProvider, IJsonSerializer jsonSerializer)
        {
            FailoverAsync? unauthorizedFailover =
                EnableUnauthorizedFailover
                    ? (response, attempt) => AuthorizationErrorsFailover(requestTimeouts, authenticationProvider, response, attempt)
                    : null;
            return new HttpRequestsFactory(
                requestTimeouts,
                (request, span) => AuthenticateRequestAsync(authenticationProvider, request, span),
                HandleApiErrors,
                unauthorizedFailover,
                clusterClient,
                jsonSerializer
            );
            
            static async Task<Request> AuthenticateRequestAsync(IAuthenticationProvider authProvider, Request request, TimeSpan timeout)
            {
                return await authProvider.AuthenticateRequestAsync(request, false, timeout).ConfigureAwait(false);
            }

            static async ValueTask<bool> HandleApiErrors(IHttpResponse response)
            {
                var status = response.Status;
                if (status.IsClientError)
                {
                    var errorResponse = await response.TryGetMessageAsync<ApiError>().ConfigureAwait(false);
                    if (errorResponse is not null && errorResponse.IsNotEmpty)
                    {
                        throw Errors.UnsuccessfulApiResponse(errorResponse);
                    }
                }

                return false;
            }

            static async Task<FailoverDecision> AuthorizationErrorsFailover(RequestTimeouts timeouts, IAuthenticationProvider authProvider, IHttpResponse response, uint attempt)
            {
                switch (attempt)
                {
                    case 0 when response.Status.IsUnauthorized:
                        await authProvider.AuthenticateAsync(true, timeouts.DefaultReadTimeout).ConfigureAwait(false);
                        return FailoverDecision.RepeatRequest;

                    default:
                        return FailoverDecision.LetItFail;
                }
            }
        }

        private class Extern : IExtern
        {
            private readonly IExternClientServices services;

            public Extern(IExternClientServices services) => this.services = services;

            public Task ReauthenticateAsync(TimeSpan? timeout) => 
                services.AuthProvider.AuthenticateAsync(true, timeout);

            public AccountListPath Accounts => new(services);
        }
    }
}