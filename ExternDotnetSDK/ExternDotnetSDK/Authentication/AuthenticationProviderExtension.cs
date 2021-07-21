using System;
using System.Threading.Tasks;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Authentication
{
    internal static class AuthenticationProviderExtension
    {
        public static async Task<Request> AuthenticateRequestAsync(this IAuthenticationProvider authenticationProvider, Request request, TimeSpan timeout)
        {
            var authenticationResult = await authenticationProvider.AuthenticateAsync(timeout).ConfigureAwait(false);
            return authenticationResult.Apply(request);
        }
    }
}