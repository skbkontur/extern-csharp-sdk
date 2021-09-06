using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Testing.ExternTestTool.Commands;
using Kontur.Extern.Client.Testing.ExternTestTool.Drive;
using Kontur.Extern.Client.Testing.ExternTestTool.Http;
using Kontur.Extern.Client.Testing.ExternTestTool.Models.Requests;
using Kontur.Extern.Client.Testing.ExternTestTool.Models.Results;
using Kontur.Extern.Client.Testing.ExternTestTool.ResponseCaching;
using Kontur.Extern.Client.Testing.Fakes.Logging;
using Kontur.Extern.Client.Testing.Lifetimes;
using Vostok.Logging.Abstractions;
using Xunit.Abstractions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Testing.ExternTestTool
{
    public class ExternTestTool
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
                                                                      PersonFullName chiefName) => 
            RunAsync(new GenerateLegalEntityAccountCommand(organizationName, chiefName, driveCertificatesReader));

        /// <summary>
        /// Генерация ФУФ ССЧ
        /// </summary>
        /// <param name="sender">Организация, которая будет оправлять документ в налоговый орган (required).</param>
        /// <param name="payer">Организация, за которую отправляется документ.</param>
        /// <param name="warrant">Сведения о доверенности.</param>
        /// <param name="generateCertificateIfAbsentInSender">Генерировать сертификат на основе данных организации-отпарвителе (<paramref name="sender"/>) и организации-объкте (<paramref name="payer"/>), если в организации-отпарвителе (<paramref name="sender"/>) не задан сертифиткат явно</param>
        /// <param name="transformFromUtf8ToCp1251">Рассматривать сгенерированный с сервером документ как UTF-8 и принудительно перекодировать его в window-1251</param>
        public Task<byte[]> GenerateFufSschFileContentAsync(Sender sender, Payer? payer = null, WarrantInfo? warrant = null, bool generateCertificateIfAbsentInSender = false, bool transformFromUtf8ToCp1251 = false) =>
            RunAsync(new GenerateFufSschXmlFileCommand(sender, payer, warrant, generateCertificateIfAbsentInSender, transformFromUtf8ToCp1251));

        /// <summary>
        /// Метод генерации тестового сертификата Сгенерированный сертификат не является КЭП.
        /// Им можно подписывать документы для отправки документооборота на тестовой площадке или тестовому роботу на боевой.
        /// </summary>
        /// <param name="data"></param>
        public Task<GeneratedCertificate> GenerateCertificateAsync(CertificateGenerationData data) =>
            RunAsync(new GenerateCertificateCommand(data));

        /// <summary>
        /// Генерация входящего письма
        /// </summary>
        /// <remarks>
        ///  Метод обращается к тестовому роботу, который генерирует письмо. Вы увидите входящий документооборот через 20 минут.  Достаточно отправить запрос один раз. Не нужно вызывать метод часто, это нагружает робота и увеличивает время получения письма.
        /// </remarks>
        /// <param name="accountId">Id учетной записи, на которую будет направлено письмо (required).</param>
        /// <param name="sender">Организация, которая будет отвечать на письмо в налоговый орган (required).</param>
        /// <param name="payer">Организация, на которую приходит письмо и за которую будет отправляться отчетность (required).</param>
        /// <param name="textOfLetter">Текст письма (required).</param>
        /// <param name="ifnsCode">Код тестовой инспекции, от которой придет требование. Возможные значения: 0007, 0008, 0084, 0085, 0087, 0088, 0093, 0094, 0096, 9979, 7702.</param>
        /// <returns>Документооборот</returns>
        public Task<IDocflowWithDocuments> GenerateCuLetterAsync(Guid accountId, Sender? sender = null, Payer? payer = null, string? textOfLetter = null, TestIfnsCode? ifnsCode = null) => 
            RunAsync(new GenerateCuLetterCommand(accountId, sender, payer, textOfLetter, ifnsCode));

        private Task<T> RunAsync<T>(IExternTestToolCommand<T> command) => 
            command.ExecuteAsync(httpClient, cache);
    }
}