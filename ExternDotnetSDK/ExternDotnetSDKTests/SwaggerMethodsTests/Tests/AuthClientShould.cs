//using NUnit.Framework;
//using Refit;

//namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
//{
//    [TestFixture]
//    internal class AuthClientShould : AllTestsShould
//    {
//        [Test]
//        public void Authorize_WithValidParameters()
//        {
//            Assert.DoesNotThrowAsync(async () => await Client.Auth.ByPass(Data.Login, Data.Password, Data.ApiKey));
//        }

//        [Test]
//        public void FailToAuthorize_WithBadLogin()
//        {
//            Assert.ThrowsAsync<ApiException>(async () => await Client.Auth.ByPass("not a login", Data.Password));
//        }

//        [Test]
//        public void FailToAuthorize_WithBadPassword()
//        {
//            Assert.ThrowsAsync<ApiException>(async () => await Client.Auth.ByPass(Data.Login, "not a password"));
//        }
//    }
//}
