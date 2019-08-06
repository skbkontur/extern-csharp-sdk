using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Certificates;
using Refit;

namespace ExternDotnetSDK.Clients.Certificates
{
    public class CertificateClient : ICertificateClient
    {
        public CertificateClient(HttpClient client)
        {
            ClientRefit = RestService.For<ICertificateClientRefit>(client);
        }

        public ICertificateClientRefit ClientRefit { get; }

        public async Task<CertificateList> GetCertificatesAsync(
            Guid accountId, int skip = 0, int take = 100, bool forAllUsers = false)
        {
            return await ClientRefit.GetCertificatesAsync(accountId, skip, take, forAllUsers);
        }
    }
}