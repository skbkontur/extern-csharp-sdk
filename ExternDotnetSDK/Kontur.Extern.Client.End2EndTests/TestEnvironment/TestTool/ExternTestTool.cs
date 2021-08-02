using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.Auth.OpenId.Provider.Models;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.Models;
using Kontur.Extern.Client.Testing.Lifetimes;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool
{
    internal class ExternTestTool
    {
        private readonly IResponseCache cache;
        private readonly HttpClient httpClient;
        private readonly DriveCertificatesReader driveCertificatesReader;

        public ExternTestTool(string apiKey, IResponseCache cache, ILifetime lifetime)
        {
            this.cache = cache;
            httpClient = lifetime.Add(new HttpClient
            {
                BaseAddress = new Uri("https://extern-api.testkontur.ru/test-tools/v1/", UriKind.Absolute)
            });
            
            httpClient.DefaultRequestHeaders.Add("X-Kontur-Apikey", apiKey);
            
            driveCertificatesReader = new DriveCertificatesReader(lifetime);
        }

        public async Task<GeneratedAccount> GenerateLegalEntityAccountAsync(string organizationName)
        {
            var responseBody = await cache.TryGetAsync(organizationName);
            if (responseBody == null)
            {
                responseBody = await GenerateNewAccount();
                await cache.SetValueAsync(organizationName, responseBody);
            }

            var response = JsonConvert.DeserializeObject<NewAccountResponse>(responseBody) ??
                           throw new InvalidOperationException("Cannot deserialize response");

            var publicPartOfCertificate = await driveCertificatesReader.GetPublicPartOfCertificate(response.CertificateInfo.CertificateDrivePath);
            return ToGeneratedAccount(response, publicPartOfCertificate);

            static GeneratedAccount ToGeneratedAccount(NewAccountResponse response, byte[] certificatePublicPart) => new(
                response.AccountId,
                response.OrganizationId,
                response.PortalUserId,
                response.RefinedRequest.Inn,
                response.RefinedRequest.Kpp,
                response.RefinedRequest.OrganizationName,
                new Credentials(response.PortalLogin, response.PortalPassword),
                response.CertificateInfo.Thumbprint,
                certificatePublicPart
            );

            async Task<string> GenerateNewAccount()
            {
                var responseMessage = await httpClient.PostAsJsonAsync(
                    "create-account-org",
                    new NewLegalEntityAccountRequest {OrganizationName = organizationName}
                );
                responseMessage.EnsureSuccessStatusCode();

                return await responseMessage.Content.ReadAsStringAsync();
            }
        }

#pragma warning disable 8618
        private class NewLegalEntityAccountRequest
        {
            public string OrganizationName { [UsedImplicitly] get; init; }
        }

        [UsedImplicitly]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public class NewAccountResponse
        {
            public RefinedRequest RefinedRequest { get; [UsedImplicitly] set; }
            public string PortalLogin { get; [UsedImplicitly] set; }
            public string PortalPassword { get; [UsedImplicitly] set; }
            public CertificateInfo CertificateInfo { get; [UsedImplicitly] set; }
            public Guid PortalUserId { get; [UsedImplicitly] set; }
            public Guid OrganizationId { get; [UsedImplicitly] set; }
            public Guid AccountId { get; [UsedImplicitly] set; }
            public Guid UserId { get; set; }
            public Guid AbonId { get; set; }
        }

        [UsedImplicitly]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public class RefinedRequest
        {
            public string Inn { get; [UsedImplicitly] set; }
            public string Kpp { get; [UsedImplicitly] set; }
            public string Innfl { get; set; }
            public string FirstName { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }
            public string OrganizationName { get; [UsedImplicitly] set; }
        }

        [UsedImplicitly]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public class CertificateInfo
        {
            public string Thumbprint { get; [UsedImplicitly] set; }
            public string CertificateDrivePath { get; set; }
        }
#pragma warning restore 8618
    }
}