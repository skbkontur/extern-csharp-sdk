using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Warrants;

namespace ExternDotnetSDK.Clients.Warrants
{
    public interface IWarrantsClient
    {
        IWarrantsClientRefit ClientRefit { get; }

        Task<WarrantList> GetWarrantsAsync(Guid accountId, int skip = 0, int take = int.MaxValue, bool forAllUsers = false);
    }
}