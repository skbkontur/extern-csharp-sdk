using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Certificates;
using Refit;

namespace ExternDotnetSDK.Clients.Certificates
{
    public class CertificateClient
    {
        private readonly ICertificateClientRefit clientRefit;

        public CertificateClient(HttpClient client)
        {
            clientRefit = RestService.For<ICertificateClientRefit>(client);
        }

        public async Task<CertificateList> GetCertificatesAsync(
            Guid accountId,
            int skip = 0,
            int take = 100,
            bool forAllUsers = false)
        {
            return await clientRefit.GetCertificatesAsync(accountId, skip, take, forAllUsers);
        }
    }
}