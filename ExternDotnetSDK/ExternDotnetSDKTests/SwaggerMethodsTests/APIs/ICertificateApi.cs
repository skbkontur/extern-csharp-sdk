using System.Threading.Tasks;
using ExternDotnetSDK.Certificates;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.APIs
{
    internal interface ICertificateApi
    {
        [Get("/v1/{accountId}/certificates?skip={skip}&take={take}&forAllUsers={forAllUsers}")]
        Task<CertificateList> GetCertificates(string accountId, long skip = 0, long take = long.MaxValue, bool forAllUsers = false);
    }
}