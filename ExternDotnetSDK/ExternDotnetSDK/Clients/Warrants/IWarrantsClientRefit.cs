using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Warrants;
using Refit;

namespace ExternDotnetSDK.Clients.Warrants
{
    public interface IWarrantsClientRefit
    {
        [Get("/v1/{accountId}/warrants")]
        Task<WarrantList> GetWarrantsAsync(Guid accountId, int skip = 0, int take = int.MaxValue, bool forAllUsers = false);
    }
}