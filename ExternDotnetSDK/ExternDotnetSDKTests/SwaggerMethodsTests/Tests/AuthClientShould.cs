using NUnit.Framework;
using Refit;

#pragma warning disable 1998

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    internal class AuthClientShould : AllTestsShould
    {
        [Test]
        public void Authorize_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await AuthClient.ByPass(Data.Login, Data.Password, Data.ApiKey));
        }

        [Test]
        public void FailToAuthorize_WithBadLogin()
        {
            Assert.ThrowsAsync<ApiException>(async () => await AuthClient.ByPass("not a login", Data.Password));
        }

        [Test]
        public void FailToAuthorize_WithBadPassword()
        {
            Assert.ThrowsAsync<ApiException>(async () => await AuthClient.ByPass(Data.Login, "not a password"));
        }
    }
}