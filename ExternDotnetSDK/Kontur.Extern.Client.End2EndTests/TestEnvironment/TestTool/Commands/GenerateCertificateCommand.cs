using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Http;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Models.Requests;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Models.Results;
using static System.Environment;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Commands
{
    internal class GenerateCertificateCommand : IExternTestToolCommand<GeneratedCertificate>
    {
        private readonly CertificateGenerationData data;

        public GenerateCertificateCommand(CertificateGenerationData data) => 
            this.data = data;

        public async Task<GeneratedCertificate> ExecuteAsync(IHttpClient httpClient, IResponseCache cache)
        {
            var responseMessage = await httpClient.PostAsJsonAsync("generate-certificate", data);
            var (certificate, privateKey, pfx) = await responseMessage.Content.ReadFromJsonAsync<GenerateCertificateResponseDto>() ??
                                                 throw await ToDeserializationException();
            return new GeneratedCertificate(certificate, privateKey, pfx);

            async Task<InvalidOperationException> ToDeserializationException()
            {
                var body = await responseMessage.Content.ReadAsStringAsync();
                return new InvalidOperationException($"Invalid generated response json: {NewLine}{body}");
            }
        }

        [UsedImplicitly]
        private record GenerateCertificateResponseDto(byte[] Certificate, string PrivateKey, byte[] Pfx);
    }
}