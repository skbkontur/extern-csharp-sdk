using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Certificates;
using Refit;

namespace ExternDotnetSDK.Clients.Certificates
{
    public class CertificateClient : ICertificateClient
    {
        public ICertificateClientRefit ClientRefit { get; }

        public CertificateClient(HttpClient client) => ClientRefit = RestService.For<ICertificateClientRefit>(client);

        public async Task<CertificateList> GetCertificatesAsync(
            Guid accountId,
            int skip = 0,
            int take = 100,
            bool forAllUsers = false) =>
            await ClientRefit.GetCertificatesAsync(accountId, skip, take, forAllUsers);
    }
}