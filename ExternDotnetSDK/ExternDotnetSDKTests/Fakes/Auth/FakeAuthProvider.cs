#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Clients.Authentication;

namespace Kontur.Extern.Client.Tests.Fakes.Auth
{
    internal class FakeAuthProvider : IAuthenticationProvider
    {
        public Task<string> GetSessionId(TimeSpan? timeout = null) => Task.FromResult("session");
    }
}