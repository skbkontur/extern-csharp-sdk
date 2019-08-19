using System;
using ExternDotnetSDK.Clients.Authentication;
using NUnit.Framework;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class DefaultAuthenticationProviderShould : AllTestsShould
    {
        [Test]
        public void Authorize_WithValidParameters()
        {
            Assert.DoesNotThrow(() =>
            {
                var authProvider = new DefaultAuthenticationProvider(Data.AuthAddress, Data.ApiKey, Data.Password, Data.Login);
                authProvider.GetSessionId();
            });
        }

        [Test]
        public void FailToAuthorize_WithBadLogin()
        {
            Assert.Throws<AggregateException>(
                () =>
                {
                    var authProvider = new DefaultAuthenticationProvider(
                        Data.AuthAddress,
                        Data.ApiKey,
                        Data.Password,
                        "not a login");
                    authProvider.GetSessionId();
                });
        }

        [Test]
        public void FailToAuthorize_WithBadPassword()
        {
            Assert.Throws<AggregateException>(
                () =>
                {
                    var authProvider = new DefaultAuthenticationProvider(
                        Data.AuthAddress,
                        Data.ApiKey,
                        "not a password",
                        Data.Login);
                    authProvider.GetSessionId();
                });
        }
    }
}