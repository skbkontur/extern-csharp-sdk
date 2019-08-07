using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Warrants;
using Refit;

namespace ExternDotnetSDK.Clients.Warrants
{
    public class WarrantsClient : IWarrantsClient
    {
        public IWarrantsClientRefit ClientRefit { get; }

        public WarrantsClient(HttpClient client) => ClientRefit = RestService.For<IWarrantsClientRefit>(client);

        public async Task<WarrantList> GetWarrantsAsync(
            Guid accountId,
            int skip = 0,
            int take = int.MaxValue,
            bool forAllUsers = false) =>
            await ClientRefit.GetWarrantsAsync(accountId, skip, take, forAllUsers);
    }
}