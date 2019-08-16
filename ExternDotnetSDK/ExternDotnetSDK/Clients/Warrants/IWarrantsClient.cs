using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Models.Warrants;

namespace ExternDotnetSDK.Clients.Warrants
{
    public interface IWarrantsClient:IHttpClient
    {
        Task<WarrantList> GetWarrantsAsync(Guid accountId, int skip = 0, int take = int.MaxValue, bool forAllUsers = false);
    }
}