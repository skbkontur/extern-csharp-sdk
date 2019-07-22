using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Certificates;
using Refit;

namespace ExternDotnetSDK.Clients.Certificates
{
    internal interface ICertificateClientRefit
    {
        [Get("/v1/{accountId}/certificates?skip={skip}&take={take}&forAllUsers={forAllUsers}")]
        Task<CertificateList> GetCertificates(Guid accountId, int skip = 0, int take = 100, bool forAllUsers = false);
    }
}