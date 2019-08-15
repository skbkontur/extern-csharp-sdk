using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Models.Certificates;

namespace ExternDotnetSDK.Clients.Certificates
{
    public interface ICertificateClient : IHttpClient
    {
        Task<CertificateList> GetCertificatesAsync(Guid accountId, int skip = 0, int take = 100, bool forAllUsers = false);
    }
}