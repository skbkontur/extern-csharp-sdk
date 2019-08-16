using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Warrants;

namespace ExternDotnetSDK.Clients.Warrants
{
    public class WarrantsClient : InnerCommonClient, IWarrantsClient
    {
        public WarrantsClient(ILogError logError, HttpClient client)
            : base(logError, client)
        {
        }

        public async Task<WarrantList> GetWarrantsAsync(
            Guid accountId,
            int skip = 0,
            int take = int.MaxValue,
            bool forAllUsers = false) =>
            await SendRequestAsync<WarrantList>(
                HttpMethod.Get,
                $"/v1/{accountId}/warrants",
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take,
                    ["forAllUsers"] = forAllUsers
                });
    }
}