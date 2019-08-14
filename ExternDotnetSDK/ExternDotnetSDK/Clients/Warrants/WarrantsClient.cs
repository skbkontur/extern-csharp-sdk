using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Warrants;
using Refit;

namespace ExternDotnetSDK.Clients.Warrants
{
    public class WarrantsClient : InnerCommonClient, IWarrantsClient
    {
        public WarrantsClient(ILogError logError, HttpClient client)
            : base(logError) =>
            ClientRefit = RestService.For<IWarrantsClientRefit>(client);

        public IWarrantsClientRefit ClientRefit { get; }

        public async Task<WarrantList> GetWarrantsAsync(
            Guid accountId,
            int skip = 0,
            int take = int.MaxValue,
            bool forAllUsers = false) => await TryExecuteTask(ClientRefit.GetWarrantsAsync(accountId, skip, take, forAllUsers));
    }
}