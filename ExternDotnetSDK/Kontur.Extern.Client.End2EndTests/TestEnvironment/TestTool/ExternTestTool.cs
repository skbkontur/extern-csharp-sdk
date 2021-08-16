using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.Models;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Commands;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Http;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Models.Requests;
using Kontur.Extern.Client.Testing.Lifetimes;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool
{
    internal class ExternTestTool
    {
        private readonly IResponseCache cache;
        private readonly IHttpClient httpClient;
        private readonly DriveCertificatesReader driveCertificatesReader;

        public ExternTestTool(string apiKey, IResponseCache cache, ILifetime lifetime)
        {
            this.cache = cache;
            httpClient = new HttpClientImplementation("https://extern-api.testkontur.ru/test-tools/v1/", apiKey, lifetime);
            
            driveCertificatesReader = new DriveCertificatesReader(lifetime);
        }

        /// <summary>
        /// Создание учетной записи экстерна для ЮЛ
        /// </summary>
        /// <param name="organizationName">Имя организации ЮЛ и выпускаемого сертификата</param>
        /// <returns>Сгенерированный аккаунт</returns>
        public Task<GeneratedAccount> GenerateLegalEntityAccountAsync(string organizationName) => 
            RunAsync(new GenerateLegalEntityAccountCommand(organizationName, driveCertificatesReader));

        /// <summary>
        /// Метод генерации тестового сертификата Сгенерированный сертификат не является КЭП.
        /// Им можно подписывать документы для отправки документооборота на тестовой площадке или тестовому роботу на боевой.
        /// </summary>
        /// <param name="data"></param>
        public Task<GeneratedCertificate> GenerateCertificateAsync(CertificateGenerationData data) =>
            RunAsync(new GenerateCertificateCommand(data));

        private Task<T> RunAsync<T>(IExternTestToolCommand<T> command) => 
            command.ExecuteAsync(httpClient, cache);
    }
}