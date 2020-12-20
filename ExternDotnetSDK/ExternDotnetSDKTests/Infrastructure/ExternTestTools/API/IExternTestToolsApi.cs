using System.Threading.Tasks;
using Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.API
{
    public interface IExternTestToolsApi
    {
        /// <summary>
        /// Получение кода подтверждения для подписания документов сертификатом
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="requestId">Идентификатор сессии для работы с сертификатами (optional)</param>
        /// <returns>Task of ApiResponse (string)</returns>
        Task<ApiResponse<string>> GetConfirmationCodeAsync(string requestId);

        /// <summary>
        /// Создание пользователя для доверительной аутентификации
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="createTestUsersRequestDto">Данные для создания пользователя и выпуска сертификата для доверительной аутентификации (optional)</param>
        /// <returns>Task of ApiResponse (CreateTestUsersResponseDto)</returns>
        Task<ApiResponse<CreateTestUsersResponseDto>> CreateAsync(CreateTestUsersRequestDto createTestUsersRequestDto = null);

        /// <summary>
        /// Создание учетной записи экстерна для ИП
        /// </summary>
        /// <remarks>
        /// Все свойства запроса необязательны. Если значение свойства не задано, то оно будет сгенерировано автоматически.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="requestDto">Данные для создания учетной записи экстерна ИП и выпускаемого сертификата (optional)</param>
        /// <returns>Task of ApiResponse (CreateExternAccountResponseDto)</returns>
        Task<ApiResponse<CreateExternAccountResponseDto>> CreateExternAccountIndividualAsync(CreateExternAccountIndividualRequestDto requestDto = null);

        /// <summary>
        /// Создание учетной записи экстерна для ЮЛ
        /// </summary>
        /// <remarks>
        /// Все свойства запроса необязательны. Если значение свойства не задано, то оно будет сгенерировано автоматически.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountOrgRequestDto">Данные для создания учетной записи экстерна ЮЛ и выпускаемого сертификата (optional)</param>
        /// <returns>Task of ApiResponse (CreateExternAccountResponseDto)</returns>
        Task<ApiResponse<CreateExternAccountResponseDto>> CreateExternAccountOrgAsync(CreateExternAccountOrgRequestDto accountOrgRequestDto = null);

        /// <summary>
        /// Метод генерации тестового сертификата
        /// </summary>
        /// <remarks>
        /// Сгенерированный сертификат не является КЭП. Им можно подписывать документы для отправки документооборота на тестовой площадке или тестовому роботу на боевой.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateCertificateRequest">Параметры для генерации тестового сертификата (optional)</param>
        /// <returns>Task of ApiResponse (CertificateAndPrivateKey)</returns>
        Task<ApiResponse<CertificateAndPrivateKey>> GenerateCertificateAsync(GenerateCertificateRequest generateCertificateRequest = null);

        /// <summary>
        /// Генерация входящего письма
        /// </summary>
        /// <remarks>
        ///  Метод обращается к тестовому роботу, который генерирует письмо. Вы увидите входящий документооборот через 20 минут.  Достаточно отправить запрос один раз. Не нужно вызывать метод часто, это нагружает робота и увеличивает время получения письма.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateCuLetterRequest">Данные для генерации письма (optional)</param>
        /// <returns>Task of ApiResponse (Docflow)</returns>
        Task<ApiResponse<Docflow>> GenerateCuLetterAsync(GenerateCuLetterRequest generateCuLetterRequest = null);

        /// <summary>
        /// Генерация входящего требования
        /// </summary>
        /// <remarks>
        /// Метод обращается к тестовому роботу, который генерирует требование. Вы увидите входящий документооборот через 20 минут.  Достаточно отправить запрос один раз. Не нужно вызывать метод часто, это нагружает робота и увеличивает время получения требования.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateDemandRequest">Данные для генерации входящего требования (optional)</param>
        /// <returns>Task of ApiResponse (Docflow)</returns>
        Task<ApiResponse<Docflow>> GenerateDemandAsync(GenerateDemandRequest generateDemandRequest = null);

        /// <summary>
        /// Генерация входящего документооборота с требованием и письмом
        /// </summary>
        /// <remarks>
        ///  Метод обращается к тестовому роботу, который генерирует документообороты. Вы увидите входящие документообороты через 20 минут.  Достаточно отправить запрос один раз. Не нужно вызывать метод часто, это нагружает робота и увеличивает время получения документооборота.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateDocflowsSuiteRequest">Данные для генерации входящего документооборота (optional)</param>
        /// <returns>Task of ApiResponse (DocflowsSuite)</returns>
        Task<ApiResponse<DocflowsSuite>> GenerateDocflowsSuiteAsync(GenerateDocflowsSuiteRequest generateDocflowsSuiteRequest = null);

        /// <summary>
        /// Генерация отчета ФНС (ФУФ ССЧ)
        /// </summary>
        /// <remarks>
        /// Метод создает исходящий документооборот в указанном статусе. Его можно использовать для быстрого создания необходимых документооборотов в учетной записи.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="fnsReportRequest"> (optional)</param>
        /// <returns>Task of ApiResponse (Docflow)</returns>
        Task<ApiResponse<Docflow>> GenerateFnsAsync(GenerateFnsReportRequest fnsReportRequest = null);

        /// <summary>
        /// Генерация ФУФ ЕНВД
        /// </summary>
        /// <remarks>
        /// Единый налог на вмененный доход
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateFufRequest">Данные об организации, которая будет оправлять документ в налоговый орган (optional)</param>
        /// <returns>Task of ApiResponse (string)</returns>
        Task<ApiResponse<string>> GenerateFufEnvdAsync(GenerateFufRequest generateFufRequest = null);

        /// <summary>
        /// Генерация ФУФ НДС с приложениями
        /// </summary>
        /// <remarks>
        /// Фуф НДС с приложениями - книгой покупок и книгой продаж
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateFufRequest">Данные об организации, которая будет оправлять документ в налоговый орган (optional)</param>
        /// <returns>Task of ApiResponse (NdsWithAttachments)</returns>
        Task<ApiResponse<NdsWithAttachments>> GenerateFufNdsWithAttachmentsAsync(GenerateFufRequest generateFufRequest = null);

        /// <summary>
        /// Генерация ФУФ налога на прибыль
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateFufRequest">Данные об организации, которая будет оправлять документ в налоговый орган (optional)</param>
        /// <returns>Task of ApiResponse (string)</returns>
        Task<ApiResponse<string>> GenerateFufProfitTaxAsync(GenerateFufRequest generateFufRequest = null);

        /// <summary>
        /// Генерация ФУФ ССЧ
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateFufRequest">Данные об организации, которая будет оправлять документ в налоговый орган (optional)</param>
        /// <returns>Task of ApiResponse (string)</returns>
        Task<ApiResponse<string>> GenerateFufSschAsync(GenerateFufRequest generateFufRequest = null);

        /// <summary>
        /// Метод возвращает календарь отчетности в формате XML. Календарь содержит даты отчетности, названия и коды форм отчетности, GFV, периоды и другую информацию.
        /// </summary>
        /// <remarks>
        /// Календарь отчетности может быть использован интеграторами.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (XmlCalendar)</returns>
        Task<ApiResponse<XmlCalendar>> GenerateXmlCalendarAsync();
    }
}