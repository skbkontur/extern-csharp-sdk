using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Authentication;
using NUnit.Framework;

#pragma warning disable 1998

namespace Kontur.Extern.Client.Tests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class AuthenticationProviderShould : AllTestsShould
    {
        private IAuthenticationProvider goodProvider;
        private IAuthenticationProvider providerWithBadLogin;
        private IAuthenticationProvider providerWithBadPassword;
        private IAuthenticationProvider providerWithBadAuthBaseAddress;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            goodProvider = new AuthenticationProvider(Data.Login, Data.Password, Data.AuthBaseAddress);
            providerWithBadLogin = new AuthenticationProvider("not a real login", Data.Password, Data.AuthBaseAddress);
            providerWithBadPassword = new AuthenticationProvider(Data.Login, "not a real password", Data.AuthBaseAddress);
            providerWithBadAuthBaseAddress = new AuthenticationProvider(
                Data.Login,
                Data.Password,
                "https://api.testkontur.ru/not_a_real_address");
        }

        [OneTimeTearDown]
        public override async Task TearDown()
        {
        }

        [Test]
        public void GetSessionId_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await goodProvider.GetSessionId());
        }

        [Test]
        public void FailToGetSessionId_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await providerWithBadLogin.GetSessionId());
            Assert.ThrowsAsync<HttpRequestException>(async () => await providerWithBadPassword.GetSessionId());
            Assert.ThrowsAsync<HttpRequestException>(async () => await providerWithBadAuthBaseAddress.GetSessionId());
        }
    }
}