using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Auth.Abstractions;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Authentication.BasicAuthorization
{
    internal class BasicAuthenticationProvider : IAuthenticationProvider
    {
        private readonly string userName;
        private readonly string password;

        public BasicAuthenticationProvider(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }
        
        public Task<IAuthenticationResult> AuthenticateAsync(TimeSpan? timeout = null)
        {
            return Task.FromResult<IAuthenticationResult>(new BasicAuthenticationResult(userName, password));
        }

        private class BasicAuthenticationResult : IAuthenticationResult
        {
            private readonly string userName;
            private readonly string password;

            public BasicAuthenticationResult(string userName, string password)
            {
                this.userName = userName;
                this.password = password;
            }

            public Request Apply(Request request) => request.WithAuthorizationHeader("Basic", $"{userName} {password}");
        }
    }
}