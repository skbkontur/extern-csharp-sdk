using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Auth.Abstractions;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Authentication
{
    internal static class AuthenticatorExtension
    {
        public static async Task<Request> AuthenticateRequestAsync (this IAuthenticator authenticator, Request request, bool force, TimeSpan timeout)
        {
            var authenticationResult = await authenticator.AuthenticateAsync(force, timeout).ConfigureAwait(false);
            return authenticationResult.Apply(request);
        }
    }
}