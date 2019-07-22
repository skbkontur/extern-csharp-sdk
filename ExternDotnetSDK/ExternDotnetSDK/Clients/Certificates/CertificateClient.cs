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
            => clientRefit = RestService.For<ICertificateClientRefit>(client);

        public async Task<CertificateList> GetCertificateListAsync(
            Guid accountId,
            int skip = 0,
            int take = 100,
            bool forAllUsers = false) 
            => await clientRefit.GetCertificates(accountId, skip, take, forAllUsers);
    }
}