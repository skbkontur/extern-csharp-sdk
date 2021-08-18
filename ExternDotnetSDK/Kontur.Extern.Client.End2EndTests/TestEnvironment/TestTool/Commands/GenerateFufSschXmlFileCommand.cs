using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Http;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Models.Requests;
using Kontur.Extern.Client.End2EndTests.TestHelpers;
using Kontur.Extern.Client.Http.Models;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Commands
{
    internal class GenerateFufSschXmlFileCommand : IExternTestToolCommand<byte[]>
    {
        private readonly Sender sender;
        private readonly Payer? payer;
        private readonly WarrantInfo? warrant;
        private readonly bool generateCertificateIfAbsent;
        private readonly bool transformFromUtf8ToCp1251;

        public GenerateFufSschXmlFileCommand(Sender sender, Payer? payer = null, WarrantInfo? warrant = null, bool generateCertificateIfAbsent = false, bool transformFromUtf8ToCp1251 = false)
        {
            this.sender = sender;
            this.payer = payer;
            this.warrant = warrant;
            this.generateCertificateIfAbsent = generateCertificateIfAbsent;
            this.transformFromUtf8ToCp1251 = transformFromUtf8ToCp1251;
        }
        
        public async Task<byte[]> ExecuteAsync(IHttpClient httpClient, IResponseCache cache)
        {
            var senderToSend = generateCertificateIfAbsent ? await ObtainCertificate(httpClient, cache) : sender;
            var xml = await GenerateFufXml(httpClient, senderToSend);
            return transformFromUtf8ToCp1251 ? TransformUtf8ToCp1251Encoding(xml) : xml;
        }

        private async Task<Sender> ObtainCertificate(IHttpClient httpClient, IResponseCache cache)
        {
            if (sender.Certificate != null)
                return sender;
            
            var certificateGenerationData = new CertificateGenerationData(
                sender.Inn,
                sender.Kpp,
                sender.OrgName,
                Surname: payer?.ChiefFullName?.Surname,
                FirstName: payer?.ChiefFullName?.Name,
                Patronymic: payer?.ChiefFullName?.Patronymic
            );
            var generatedCertificate = await new GenerateCertificateCommand(certificateGenerationData).ExecuteAsync(httpClient, cache);
            return sender with
            {
                Certificate = Base64String.Encode(generatedCertificate.PublicKey)
            };
        }
        
        private async Task<byte[]> GenerateFufXml(IHttpClient httpClient, Sender senderToSend)
        {
            var request = CreateGenerateFufRequest();
            var responseMessage = await httpClient.PostAsJsonAsync("generate-fuf-ssch?windows1251=true", request);
            return await responseMessage.Content.ReadAsByteArrayAsync();
            
            GenerateFufRequest CreateGenerateFufRequest()
            {
                var senderDto = new SenderDto(
                    senderToSend.Inn,
                    new CertificateDto(senderToSend.Certificate!.Value.ToString()),
                    senderToSend.Kpp,
                    senderToSend.OrgName
                );
                var payerDto = payer == null
                    ? null
                    : new PayerDto(
                        payer.Inn,
                        payer.Name,
                        payer.Kpp == null ? null : new OrganizationInfoDto(payer.Kpp),
                        payer.ChiefFullName
                    );
                return new GenerateFufRequest(senderDto, payerDto, warrant);
            }
        }
        
        private static byte[] TransformUtf8ToCp1251Encoding(byte[] xml)
        {
            var expectedEncoding = Encoding.GetEncoding("windows-1251");
            var xmlString = Encoding.UTF8.GetString(xml);
            var document = XDocument.Parse(xmlString);
            var xmlEncodingName = document.Declaration?.Encoding;
            if (xmlEncodingName == null)
                return xml;

            if (document.Declaration!.Encoding != expectedEncoding.WebName)
            {
                document.Declaration!.Encoding = expectedEncoding.WebName;
                return expectedEncoding.GetBytes(document.ToStringWithDeclaration(expectedEncoding));
            }

            return expectedEncoding.GetBytes(xmlString);
        }

        private record CertificateDto(string Content);
        private record SenderDto(string Inn, CertificateDto? Certificate, string? Kpp, string? Name);
        private record OrganizationInfoDto(string? Kpp);
        private record PayerDto(string Inn, string? Name, OrganizationInfoDto? Organization, PersonFullName? ChiefFio);
        private record GenerateFufRequest(SenderDto sender, PayerDto? payer, WarrantInfo? warrant);
    }
}