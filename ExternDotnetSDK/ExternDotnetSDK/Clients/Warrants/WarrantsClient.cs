using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Warrants;
using Refit;

namespace ExternDotnetSDK.Clients.Warrants
{
    public class WarrantsClient
    {
        private readonly IWarrantsClientRefit clientRefit;

        public WarrantsClient(HttpClient client)
        {
            clientRefit = RestService.For<IWarrantsClientRefit>(client);
        }

        public async Task<WarrantList> GetWarrantsAsync(
            Guid accountId,
            int skip = 0,
            int take = int.MaxValue,
            bool forAllUsers = false)
        {
            return await clientRefit.GetWarrantsAsync(accountId, skip, take, forAllUsers);
        }
    }
}