using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Drafts;
using ExternDotnetSDK.Drafts.Requests;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class DraftClientShould : AllTestsShould
    {
        private DraftClient draftClient;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp();
            draftClient = new DraftClient(Client);
        }

        [Test]
        public async Task CreateDraft_WithValidParameters()
        {
            var draftMetaRequest = new DraftMetaRequest
            {
                Payer = new AccountInfoRequest
                {
                    Inn = "4670239313",
                    Organization = new OrganizationInfoRequest {Kpp = "601501836"}
                },
                Sender = new SenderRequest
                {
                    Inn = "3347632347",
                    Kpp = "563045648",
                    IpAddress = "81.211.0.226"
                },
                Recipient = new RecipientInfoRequest
                {
                    FssCode = "11111"
                }
            };
            try
            {
                var t = await draftClient.CreateDraftAsync(Account.Id, draftMetaRequest);
            }
            catch (ApiException)
            {
            }
        }
    }
}