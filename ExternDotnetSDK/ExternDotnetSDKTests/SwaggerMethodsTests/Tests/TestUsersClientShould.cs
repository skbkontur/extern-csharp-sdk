using System.Threading.Tasks;
using ExternDotnetSDK.Clients.TestUsers;
using ExternDotnetSDK.Test;
using FluentAssertions;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class TestUsersClientShould : AllTestsShould
    {
        private TestUsersClient client;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp();
            client = new TestUsersClient(Client);
        }

        [Test]
        public void CreateUser_WithValidParameters()
        {
            const string phone = "1234567890";
            var request = new CreateTestUsersRequestDto
            {
                Phone = phone
            };
            CreateTestUsersResponseDto user = null;
            Assert.DoesNotThrowAsync(async () => user = await client.CreateTestUserAsync(request));
            user.Phone.Should().BeEquivalentTo(phone);
            user.Inn.Should().NotBeNull();
            user.Kpp.Should().NotBeNull();
            user.FirstName.Should().NotBeNull();
            user.Surname.Should().NotBeNull();
            user.Patronymic.Should().NotBeNull();
            user.OrganizationName.Should().NotBeNull();
            user.PortalUserId.Should().NotBeEmpty();
        }

        [TestCase("123456789")]
        [TestCase("123456789", "3565487840")]
        [TestCase("123456789", "3565487844", "560644400")]
        public void FailToCreateUser_WithBadParameters(string phone, string inn = null, string kpp = null) =>
            Assert.ThrowsAsync<ApiException>(
                async () => await client.CreateTestUserAsync(
                    new CreateTestUsersRequestDto
                    {
                        Phone = phone,
                        Kpp = kpp,
                        Inn = inn
                    }));
    }
}