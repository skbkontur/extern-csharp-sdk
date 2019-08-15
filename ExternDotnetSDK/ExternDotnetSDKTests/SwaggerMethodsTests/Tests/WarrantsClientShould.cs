//using System;
//using NUnit.Framework;
//using Refit;

//namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
//{
//    [TestFixture]
//    internal class WarrantsClientShould : AllTestsShould
//    {
//        [Test]
//        public void FailToGetWarrants_WithBadAccountId()
//        {
//            Assert.ThrowsAsync<ApiException>(async () => await Client.Warrants.GetWarrantsAsync(Guid.NewGuid()));
//        }

//        [Test]
//        public void GetWarrants_WithValidParameters()
//        {
//            Assert.DoesNotThrowAsync(async () => await Client.Warrants.GetWarrantsAsync(Account.Id));
//        }

//        [TestCase(-1)]
//        [TestCase(0, 0)]
//        [TestCase(0, -1)]
//        public void FailToGetWarrants_WithBadQueryParameters(int skip = 0, int take = int.MaxValue)
//        {
//            Assert.ThrowsAsync<ApiException>(async () => await Client.Warrants.GetWarrantsAsync(Account.Id, skip, take));
//        }
//    }
//}