using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Certificates;

namespace ExternDotnetSDK.Clients.Certificates
{
    public interface ICertificateClient
    {
        ICertificateClientRefit ClientRefit { get; }

        Task<CertificateList> GetCertificatesAsync(Guid accountId, int skip = 0, int take = 100, bool forAllUsers = false);
    }
}