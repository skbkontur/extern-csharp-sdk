using System;
using ExternDotnetSDKTests.SwaggerMethodsTests.Common;
using NUnit.Framework;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    internal class IAuthenticationProviderShould : AllTestsShould
    {
        [Test]
        public void Authorize_WithValidParameters()
        {
            Assert.DoesNotThrow(
                () =>
                {
                    var authProvider = new MyAuthenticationProvider(Data.AuthAddress, Data.ApiKey, Data.Password, Data.Login);
                    authProvider.GetSessionId();
                });
        }

        [Test]
        public void FailToAuthorize_WithBadLogin()
        {
            Assert.Throws<AggregateException>(
                () =>
                {
                    var authProvider = new MyAuthenticationProvider(
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
                    var authProvider = new MyAuthenticationProvider(
                        Data.AuthAddress,
                        Data.ApiKey,
                        "not a password",
                        Data.Login);
                    authProvider.GetSessionId();
                });
        }
    }
}