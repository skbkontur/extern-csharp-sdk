using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.Clients.Authentication;
using Kontur.Extern.Client.Clients.Authentication.Client;
using Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests;
using Kontur.Extern.Client.Clients.Authentication.Providers;
using Kontur.Extern.Client.Clients.Authentication.TokenAuth.Kontur.Extern.Client.Clients.Authentication;
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
            goodProvider = new OpenIdPasswordAuthenticationProvider(
                Data.AuthBaseAddress,
                new PasswordTokenRequest() {Password = Data.Password, UserName = Data.Login, ClientId = Data.ClientId, ClientSecret = Data.ApiKey});

            providerWithBadLogin = new OpenIdPasswordAuthenticationProvider(Data.AuthBaseAddress, new PasswordTokenRequest() {Password = Data.Password, UserName = "not a real login", ClientId = Data.ClientId, ClientSecret = Data.ApiKey});
            providerWithBadPassword = new OpenIdPasswordAuthenticationProvider(Data.AuthBaseAddress, new PasswordTokenRequest() {Password = "not a real password", UserName = Data.Login, ClientId = Data.ClientId});
            providerWithBadAuthBaseAddress = new OpenIdPasswordAuthenticationProvider(
                "https://api.testkontur.ru/not_a_real_address",
                new PasswordTokenRequest() {Password = Data.Password, UserName = Data.Login, ClientId = Data.ClientId});
        }

        [OneTimeTearDown]
        public override async Task TearDown()
        {
        }

        [Test]
        public async Task GetAuthToken_WithValidParameters()
        {
            var result = await goodProvider.AuthenticateAsync();

            result.Success.Should().BeTrue(result.ErrorMessage);
        }

        [Test]
        public async Task FailToGetAuthToken_WithBadParameters()
        {
            var providersResults = new List<ServiceResult>()
            {
                await providerWithBadLogin.AuthenticateAsync(),
                await providerWithBadPassword.AuthenticateAsync(),
                await providerWithBadAuthBaseAddress.AuthenticateAsync()
            };

            providersResults.ForEach(r => r.Success.Should().BeTrue(r.ErrorMessage));
        }
    }
}