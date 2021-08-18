using System.Text;
using System.Threading.Tasks;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.Models;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Commands;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Http;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Models.Requests;
using Kontur.Extern.Client.Testing.Fakes.Logging;
using Kontur.Extern.Client.Testing.Lifetimes;
using Vostok.Logging.Abstractions;
using Xunit.Abstractions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool
{
    internal class ExternTestTool
    {
        private readonly string apiKey;
        private readonly IResponseCache cache;
        private readonly ILifetime lifetime;
        private readonly IHttpClient httpClient;
        private readonly DriveCertificatesReader driveCertificatesReader;

        public ExternTestTool(string apiKey, IResponseCache cache, ILifetime lifetime, ILog log)
        {
            this.apiKey = apiKey;
            this.cache = cache;
            this.lifetime = lifetime;
            httpClient = new HttpClientImplementation("https://extern-api.testkontur.ru/test-tools/v1/", apiKey, lifetime, log);
            
            driveCertificatesReader = new DriveCertificatesReader(lifetime);
        }

        public ExternTestTool WithLoggingTo(ITestOutputHelper output) => new(apiKey, cache, lifetime, new TestLog(output));

        /// <summary>
        /// Создание учетной записи экстерна для ЮЛ
        /// </summary>
        /// <param name="organizationName">Имя организации ЮЛ и выпускаемого сертификата</param>
        /// <param name="chiefName">Имя руководителя</param>
        /// <returns>Сгенерированный аккаунт</returns>
        public Task<GeneratedAccount> GenerateLegalEntityAccountAsync(string organizationName, 
                                                                      (string surname, string firstName, string partonymicName) chiefName) => 
            RunAsync(new GenerateLegalEntityAccountCommand(organizationName, chiefName, driveCertificatesReader));

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