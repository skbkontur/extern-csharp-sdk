using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Auth.Abstractions;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Authentication
{
    internal static class AuthenticationProviderExtension
    {
        public static async Task<Request> AuthenticateRequestAsync (this IAuthenticationProvider authenticationProvider, Request request, bool force, TimeSpan timeout)
        {
            var authenticationResult = await authenticationProvider.AuthenticateAsync(force, timeout).ConfigureAwait(false);
            return authenticationResult.Apply(request);
        }
    }
}