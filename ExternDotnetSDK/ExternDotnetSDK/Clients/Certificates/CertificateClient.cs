using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Certificates;

namespace ExternDotnetSDK.Clients.Certificates
{
    public class CertificateClient : InnerCommonClient, ICertificateClient
    {
        public CertificateClient(ILogError logError, HttpClient client)
            : base(logError, client)
        {
        }

        public async Task<CertificateList> GetCertificatesAsync(
            Guid accountId,
            int skip = 0,
            int take = 100,
            bool forAllUsers = false) =>
            await SendRequestAsync<CertificateList>(
                HttpMethod.Get,
                $"/v1/{accountId}/certificates",
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take,
                    ["forAllUsers"] = forAllUsers,
                });
    }
}