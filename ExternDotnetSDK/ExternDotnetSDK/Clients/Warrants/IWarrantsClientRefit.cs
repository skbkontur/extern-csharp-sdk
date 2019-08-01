using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Warrants;
using Refit;

namespace ExternDotnetSDK.Clients.Warrants
{
    internal interface IWarrantsClientRefit
    {
        [Get("/v1/{accountId}/warrants")]
        Task<WarrantList> GetWarrantsAsync(Guid accountId, int skip = 0, int take = int.MaxValue, bool forAllUsers = false);
    }
}