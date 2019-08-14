using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Certificates;
using Refit;

namespace ExternDotnetSDK.Clients.Certificates
{
    public class CertificateClient : InnerCommonClient, ICertificateClient
    {
        public CertificateClient(ILogError logError, HttpClient client)
            : base(logError) =>
            ClientRefit = RestService.For<ICertificateClientRefit>(client);

        public ICertificateClientRefit ClientRefit { get; }

        public async Task<CertificateList> GetCertificatesAsync(
            Guid accountId,
            int skip = 0,
            int take = 100,
            bool forAllUsers = false) =>
            await TryExecuteTask(ClientRefit.GetCertificatesAsync(accountId, skip, take, forAllUsers));
    }
}