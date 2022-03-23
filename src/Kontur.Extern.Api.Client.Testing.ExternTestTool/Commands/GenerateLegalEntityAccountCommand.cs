using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Testing.ExternTestTool.Drive;
using Kontur.Extern.Api.Client.Testing.ExternTestTool.Http;
using Kontur.Extern.Api.Client.Testing.ExternTestTool.Models.Results;
using Kontur.Extern.Api.Client.Testing.ExternTestTool.ResponseCaching;
using Newtonsoft.Json;

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.Commands
{
    internal class GenerateLegalEntityAccountCommand : IExternTestToolCommand<GeneratedAccount>
    {
        private readonly string organizationName;
        private readonly PersonFullName chiefName;
        private readonly DriveCertificatesReader driveCertificatesReader;

        public GenerateLegalEntityAccountCommand(
            string organizationName, 
            PersonFullName chiefName, 
            DriveCertificatesReader driveCertificatesReader)
        {
            this.organizationName = organizationName;
            this.chiefName = chiefName;
            this.driveCertificatesReader = driveCertificatesReader;
        }
        
        public async Task<GeneratedAccount> ExecuteAsync(IHttpClient httpClient, IResponseCache cache)
        {
            var responseBody = await cache.TryGetAsync(organizationName) ?? await GenerateNewAccount();
            var response = DeserializeAccount(responseBody);

            var publicPartOfCertificate = await driveCertificatesReader.GetPublicPartOfCertificate(response.CertificateInfo.CertificateDrivePath);
            if (publicPartOfCertificate == null)
            {
                responseBody = await GenerateNewAccount();
                response = DeserializeAccount(responseBody);
                publicPartOfCertificate = await driveCertificatesReader.GetPublicPartOfCertificate(response.CertificateInfo.CertificateDrivePath) ??
                                          throw new InvalidOperationException("The content of newly generated account was not found");
            }

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
                    new NewLegalEntityAccountRequest
                    {
                        OrganizationName = organizationName,
                        FirstName = chiefName.Name,
                        Surname = chiefName.Surname,
                        Patronymic = chiefName.Patronymic
                    }
                );
                
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                await cache.SetValueAsync(organizationName, responseJson);
                return responseJson;
            }

            static NewAccountResponse DeserializeAccount(string responseBody)
            {
                return JsonConvert.DeserializeObject<NewAccountResponse>(responseBody) ??
                       throw new InvalidOperationException("Cannot deserialize response");
            }
        }

#pragma warning disable 8618
        private class NewLegalEntityAccountRequest
        {
            public string OrganizationName { [UsedImplicitly] get; init; }
            public string FirstName { [UsedImplicitly] get; set; }
            public string Surname { [UsedImplicitly] get; set; }
            public string Patronymic { [UsedImplicitly] get; set; }
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
            public string CertificateDrivePath { get; [UsedImplicitly] set; }
        }
#pragma warning restore 8618
    }
}