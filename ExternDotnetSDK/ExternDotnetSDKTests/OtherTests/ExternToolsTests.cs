using System.Net;
using System.Threading.Tasks;
using Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.API;
using Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.OtherTests
{
    [TestFixture]
    public class ExternToolsTests
    {
        private readonly string CertificateContent = "MIIIszCCCGCgAwIBAgIQCuFIAQSsMqFGALlkW2HXxzAKBggqhQMHAQEDAjCB/jEe\r\nMBwGCSqGSIb3DQEJARYPbm9yZXBseUB0ZXN0LnJ1MRgwFgYFKoUDZAESDTAwMDAw\r\nMDAwMDAwMDAxGjAYBggqhQMDgQMBARIMMDAwMDAwMDAwMDAwMQswCQYDVQQGEwJS\r\nVTESMBAGA1UECAwJNjYg0KHQstCeMRMwEQYDVQQHDArQldCx0YPRgNCzMRkwFwYD\r\nVQQJDBDRg9C7LiDQnC4sINC0LiA1MQ0wCwYDVQQLDATQo9CmMSIwIAYDVQQKDBnQ\r\nntCe0J4gItCQ0LLRgtC+0YLQtdGB0YIiMSIwIAYDVQQDDBnQntCe0J4gItCQ0LLR\r\ngtC+0YLQtdGB0YIiMB4XDTIwMDcyNjE5NDcyNVoXDTIxMTAyNjE5NDcyNVowggFO\r\nMRgwFgYIKoUDA4ENAQESCjAwMDAwMDAwMTIxMjAwBgkqhkiG9w0BCQIMIzUyOTI3\r\nOTM4MjQwNy01MjkyMTQzODktMDI1Nzg2MDMxNTAwMRgwFgYFKoUDZAUSDTEwNjEy\r\nMTMyODk2MjAxPjA8BgkqhkiG9w0BCQEWL2JkODY1YTZhLTQ0YWUtNDQ1NS1iNGI0\r\nLWU0NWY4ZGRlOTRhMkBkZXYua29udHVyMRowGAYIKoUDA4EDAQESDDUyOTI3OTM4\r\nMjQwNzEWMBQGBSqFA2QDEgsyNTc4NjAzMTUwMDEsMCoGA1UEKgwj0JDQvdC90LAg\r\n0JDQu9C10LrRgdCw0L3QtNGA0L7QstC90LAxFzAVBgNVBAQMDtCY0LLQsNC90L7Q\r\nstCwMSkwJwYDVQQDDCAoVGVzdENvcmUpINCe0JDQniDQoNC+0LzQsNGI0LrQsDBm\r\nMB8GCCqFAwcBAQEBMBMGByqFAwICJAAGCCqFAwcBAQICA0MABECM0vWbRTrGpbxx\r\nSyubA+9FVV9XEcqxJ1WRw91H01DDns0zm7027g2ZIxiDRAUNnQbzxkDTG6ZtwYZq\r\nXqm72/eko4IFXjCCBVowDgYDVR0PAQH/BAQDAgTwMHIGA1UdEQRrMGmBL2JkODY1\r\nYTZhLTQ0YWUtNDQ1NS1iNGI0LWU0NWY4ZGRlOTRhMkBkZXYua29udHVypDYwNDEY\r\nMBYGCCqFAwOBDQEBEgowMDAwMDAwMDEyMRgwFgYFKoUDZAUSDTEwNjEyMTMyODk2\r\nMjAwEwYDVR0gBAwwCjAIBgYqhQNkcQEwQQYDVR0lBDowOAYIKwYBBQUHAwIGByqF\r\nAwICIgYGCCsGAQUFBwMEBgcqhQMDBwgBBggqhQMDBwEBAQYGKoUDAwcBMIHGBggr\r\nBgEFBQcBAQSBuTCBtjBgBggrBgEFBQcwAoZUaHR0cDovL3VjLWF1dG90ZXN0LTEy\r\nLnNhbmRib3gubG9jYWwvYWlhLzBiNmRjNmI0OTY0MjhiYWFkNWQ1ZTY3ZGFmMWM3\r\nMWVkNGU0NzhmYTQuY3J0MFIGCCsGAQUFBzAChkZodHRwOi8vdWMtYXV0b3Rlc3Qt\r\nMTIvYWlhLzBiNmRjNmI0OTY0MjhiYWFkNWQ1ZTY3ZGFmMWM3MWVkNGU0NzhmYTQu\r\nY3J0MCsGA1UdEAQkMCKADzIwMjAwNzI2MTk0NzI1WoEPMjAyMTEwMjYxOTQ3MjVa\r\nMIIBMwYFKoUDZHAEggEoMIIBJAwrItCa0YDQuNC/0YLQvtCf0YDQviBDU1AiICjQ\r\nstC10YDRgdC40Y8gNC4wKQxTItCj0LTQvtGB0YLQvtCy0LXRgNGP0Y7RidC40Lkg\r\n0YbQtdC90YLRgCAi0JrRgNC40L/RgtC+0J/RgNC+INCj0KYiINCy0LXRgNGB0LjQ\r\nuCAyLjAMT9Ch0LXRgNGC0LjRhNC40LrQsNGCINGB0L7QvtGC0LLQtdGC0YHRgtCy\r\n0LjRjyDihJYg0KHQpC8xMjQtMzU3MCDQvtGCIDE0LjEyLjIwMTgMT9Ch0LXRgNGC\r\n0LjRhNC40LrQsNGCINGB0L7QvtGC0LLQtdGC0YHRgtCy0LjRjyDihJYg0KHQpC8x\r\nMjgtMjk4MyDQvtGCIDE4LjExLjIwMTYwNgYFKoUDZG8ELQwrItCa0YDQuNC/0YLQ\r\nvtCf0YDQviBDU1AiICjQstC10YDRgdC40Y8gNC4wKTCBtQYDVR0fBIGtMIGqMFqg\r\nWKBWhlRodHRwOi8vdWMtYXV0b3Rlc3QtMTIuc2FuZGJveC5sb2NhbC9jZHAvMGI2\r\nZGM2YjQ5NjQyOGJhYWQ1ZDVlNjdkYWYxYzcxZWQ0ZTQ3OGZhNC5jcmwwTKBKoEiG\r\nRmh0dHA6Ly91Yy1hdXRvdGVzdC0xMi9jZHAvMGI2ZGM2YjQ5NjQyOGJhYWQ1ZDVl\r\nNjdkYWYxYzcxZWQ0ZTQ3OGZhNC5jcmwwggE+BgNVHSMEggE1MIIBMYAUC23GtJZC\r\ni6rV1eZ9rxxx7U5Hj6ShggEFpIIBATCB/jEeMBwGCSqGSIb3DQEJARYPbm9yZXBs\r\neUB0ZXN0LnJ1MRgwFgYFKoUDZAESDTAwMDAwMDAwMDAwMDAxGjAYBggqhQMDgQMB\r\nARIMMDAwMDAwMDAwMDAwMQswCQYDVQQGEwJSVTESMBAGA1UECAwJNjYg0KHQstCe\r\nMRMwEQYDVQQHDArQldCx0YPRgNCzMRkwFwYDVQQJDBDRg9C7LiDQnC4sINC0LiA1\r\nMQ0wCwYDVQQLDATQo9CmMSIwIAYDVQQKDBnQntCe0J4gItCQ0LLRgtC+0YLQtdGB\r\n0YIiMSIwIAYDVQQDDBnQntCe0J4gItCQ0LLRgtC+0YLQtdGB0YIighA8C+gA1qlo\r\nqE/q4e55QKJqMB0GA1UdDgQWBBRIGsprvWL8bYrxyhGVoNzvj8WpTjAKBggqhQMH\r\nAQEDAgNBADApxp5RSvAwOzAVOn1kUCTzUvAOcXZtkmpOArCbA5YtcnAb9Nx905/x\r\nl6Tm500EzM/xN0AQLieMSCppYFWZmQU=";
        private ExternTestToolsApi instance;

        [SetUp]
        public void Init()
        {
            var conf = new Configuration("http://extern-api.testkontur.ru", "", "", "");
            instance = new ExternTestToolsApi(conf);
        }

        /// <summary>
        /// Test CreateExternAccountIndividual
        /// </summary>
        [Test]
        public void CreateExternAccountIndividualTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //CreateExternAccountIndividualRequestDto request = null;
            //var response = instance.CreateExternAccountIndividual(request);
            //Assert.IsInstanceOf<CreateExternAccountResponseDto> (response, "response is CreateExternAccountResponseDto");
        }

        /// <summary>
        /// Test CreateExternAccountOrg
        /// </summary>
        [Test]
        public void CreateExternAccountOrgTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            // var response = await instance.CreateExternAccountOrgAsync();
            // Assert.IsInstanceOf<CreateExternAccountResponseDto>(response.Data, "response is CreateExternAccountResponseDto");
            // Assert.AreEqual(response.Data, HttpStatusCode.OK);
        }

        /// <summary>
        /// Test GenerateCertificate
        /// </summary>
        [Test]
        public async Task GenerateCertificateTest()
        {
            var generateCertificateRequest = new GenerateCertificateRequest("183501166447", "525601001", "Romashka");
            var response = await instance.GenerateCertificateAsync(generateCertificateRequest);
            Assert.IsInstanceOf<CertificateAndPrivateKey>(response.Data, "response is CertificateAndPrivateKey");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        /// <summary>
        /// Test GenerateCuLetter
        /// </summary>
        [Test]
        public void GenerateCuLetterTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //GenerateCuLetterRequest generateCuLetterRequest = null;
            //var response = instance.GenerateCuLetter(generateCuLetterRequest);
            //Assert.IsInstanceOf<Docflow> (response, "response is Docflow");
        }

        /// <summary>
        /// Test GenerateDemand
        /// </summary>
        [Test]
        public void GenerateDemandTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //GenerateDemandRequest generateDemandRequest = null;
            //var response = instance.GenerateDemand(generateDemandRequest);
            //Assert.IsInstanceOf<Docflow> (response, "response is Docflow");
        }

        /// <summary>
        /// Test GenerateDocflowsSuite
        /// </summary>
        [Test]
        public void GenerateDocflowsSuiteTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //GenerateDocflowsSuiteRequest generateDocflowsSuiteRequest = null;
            //var response = instance.GenerateDocflowsSuite(generateDocflowsSuiteRequest);
            //Assert.IsInstanceOf<DocflowsSuite> (response, "response is DocflowsSuite");
        }

        /// <summary>
        /// Test GenerateFns
        /// </summary>
        [Test]
        public void GenerateFnsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            // var fnsReportRequest = new GenerateFnsReportRequest();
            // var response = await instance.GenerateFnsAsync(fnsReportRequest);
            // Assert.IsInstanceOf<Docflow>(response, "response is Docflow");
            // Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        /// <summary>
        /// Test GenerateFufEnvd
        /// </summary>
        [Test]
        public async Task GenerateFufEnvdTest()
        {
            var generateFufRequest = new GenerateFufRequest(new Sender("183501166447", "525601001", "Romashka", new Certificate(CertificateContent)));
            var response = await instance.GenerateFufEnvdAsync(generateFufRequest);
            Assert.IsInstanceOf<string>(response.Data, "response is string");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        /// <summary>
        /// Test GenerateFufNdsWithAttachments
        /// </summary>
        [Test]
        public async Task GenerateFufNdsWithAttachmentsTest()
        {
            var generateFufRequest = new GenerateFufRequest(new Sender("183501166447", "525601001", "Romashka", new Certificate(CertificateContent)));
            var response = await instance.GenerateFufNdsWithAttachmentsAsync(generateFufRequest);
            Assert.IsInstanceOf<NdsWithAttachments>(response.Data, "response is NdsWithAttachments");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        /// <summary>
        /// Test GenerateFufProfitTax
        /// </summary>
        [Test]
        public async Task GenerateFufProfitTaxTest()
        {
            var generateFufRequest = new GenerateFufRequest(new Sender("183501166447", "525601001", "Romashka", new Certificate(CertificateContent)));
            var response = await instance.GenerateFufProfitTaxAsync(generateFufRequest);
            Assert.IsInstanceOf<string>(response.Data, "response is string");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        /// <summary>
        /// Test GenerateFufSsch
        /// </summary>
        [Test]
        public async Task GenerateFufSschTest()
        {
            var generateFufRequest = new GenerateFufRequest(new Sender("183501166447", "525601001", "Romashka", new Certificate(CertificateContent)));
            var response = await instance.GenerateFufSschAsync(generateFufRequest);
            Assert.IsInstanceOf<string>(response.Data, "response is string");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        /// <summary>
        /// Test GenerateXmlCalendar
        /// </summary>
        [Test]
        public async Task GenerateXmlCalendarTest()
        {
            var response = await instance.GenerateXmlCalendarAsync();
            Assert.IsInstanceOf<XmlCalendar>(response.Data, "response is XmlCalendar");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        /// Test GetConfirmationCode
        /// <summary>
        /// </summary>
        [Test]
        public void GetUnknownConfirmationCodeTest()
        {
            var ex = Assert.ThrowsAsync<ApiException>(async () => await instance.GetConfirmationCodeAsync("12345"));
            Assert.AreEqual(ex.ErrorCode, 400);
        }
    }
}