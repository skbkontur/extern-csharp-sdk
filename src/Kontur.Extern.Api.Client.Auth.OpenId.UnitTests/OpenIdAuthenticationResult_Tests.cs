using FluentAssertions;
using Kontur.Extern.Api.Client.Auth.OpenId.Provider;
using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Time;
using Xunit;

namespace Kontur.Extern.Api.Client.Auth.OpenId.UnitTests
{
    public class OpenIdAuthenticationResult_Tests
    {
        [Fact]
        public void Should_set_auth_header_to_the_request()
        {
            var authResult = new OpenIdAuthenticationResult("access-token", 1.Seconds());

            var request = authResult.Apply(Request.Get("/some-url"));

            var authorizationHeader = request.Headers?.Authorization;
            authorizationHeader?.Should().Be("Bearer access-token");
        }
    }
}