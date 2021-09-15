#nullable disable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Testing.ExternTestTool.LegacyExternTestTools.Model;
#pragma warning disable 219

#pragma warning disable 618

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.LegacyExternTestTools.API
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class ExternTestToolsApi : IExternTestToolsApi
    {
        /// <summary>
        /// Получение кода подтверждения для подписания документов сертификатом 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="requestId">Идентификатор сессии для работы с сертификатами (optional)</param>
        /// <returns>Task of ApiResponse (string)</returns>
        public Task<ApiResponse<string>> GetConfirmationCodeAsync(string requestId)
        {
            var localPath = "/test-tools/v1/get-cloud-sign-confirmation-code";
            throw new NotImplementedException();
            // var sessionId = await Configuration.AuthenticationProvider.GetSessionId().ConfigureAwait(false);
            //
            // var request = new RestRequest(localPath, Method.GET)
            //     .AddQueryParameter("requestId", requestId)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey)
            //     .AddHeader("Authorization", $"auth.sid {sessionId}");
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            //
            // var localVarStatusCode = (int) response.StatusCode;
            //
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("GetConfirmationCode", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<string>(
            //     localVarStatusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (string) HttpHelper.Deserialize(response, typeof (string)));
        }

        /// <summary>
        /// Создание пользователя для доверительной аутентификации 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="createTestUsersRequestDto">Данные для создания пользователя и выпуска сертификата для доверительной аутентификации (optional)</param>
        /// <returns>Task of ApiResponse (CreateTestUsersResponseDto)</returns>
        public  Task<ApiResponse<CreateTestUsersResponseDto>> CreateAsync(CreateTestUsersRequestDto createTestUsersRequestDto = null)
        {
            var localPath = "/test-tools/v1/create-test-user-for-trusted";
            throw new NotImplementedException();
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey);
            //
            // if (createTestUsersRequestDto != null)
            // {
            //     request.AddJsonBody(createTestUsersRequestDto);
            // }
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            //
            // var localVarStatusCode = (int) response.StatusCode;
            //
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("Create", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<CreateTestUsersResponseDto>(
            //     localVarStatusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (CreateTestUsersResponseDto) HttpHelper.Deserialize(response, typeof (CreateTestUsersResponseDto)));
        }

        /// <summary>
        /// Создание учетной записи экстерна для ИП Все свойства запроса необязательны. Если значение свойства не задано, то оно будет сгенерировано автоматически.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="requestDto">Данные для создания учетной записи экстерна ИП и выпускаемого сертификата (optional)</param>
        /// <returns>Task of ApiResponse (CreateExternAccountResponseDto)</returns>
        public  Task<ApiResponse<CreateExternAccountResponseDto>> CreateExternAccountIndividualAsync(CreateExternAccountIndividualRequestDto requestDto = null)
        {
            var localPath = "/test-tools/v1/create-account-individual";
            throw new NotImplementedException();
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey);
            //
            // if (requestDto != null)
            // {
            //     request.AddJsonBody(requestDto);
            // }
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            //
            // var localVarStatusCode = (int) response.StatusCode;
            //
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("CreateExternAccountIndividual", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<CreateExternAccountResponseDto>(
            //     localVarStatusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (CreateExternAccountResponseDto) HttpHelper.Deserialize(response, typeof (CreateExternAccountResponseDto)));
        }

        /// <summary>
        /// Создание учетной записи экстерна для ЮЛ Все свойства запроса необязательны. Если значение свойства не задано, то оно будет сгенерировано автоматически.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountOrgRequestDto">Данные для создания учетной записи экстерна ЮЛ и выпускаемого сертификата (optional)</param>
        /// <returns>Task of ApiResponse (CreateExternAccountResponseDto)</returns>
        public  Task<ApiResponse<CreateExternAccountResponseDto>> CreateExternAccountOrgAsync(CreateExternAccountOrgRequestDto accountOrgRequestDto = null)
        {
            var localPath = "/test-tools/v1/create-account-org";
            throw new NotImplementedException();
            // var sessionId = await Configuration.AuthenticationProvider.GetSessionId().ConfigureAwait(false);
            //
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("Authorization", $"auth.sid {sessionId}")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey);
            //
            // if (accountOrgRequestDto != null)
            // {
            //     request.AddJsonBody(accountOrgRequestDto);
            // }
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            // var localVarStatusCode = (int) response.StatusCode;
            //
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("CreateExternAccountOrg", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<CreateExternAccountResponseDto>(
            //     localVarStatusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (CreateExternAccountResponseDto) HttpHelper.Deserialize(response, typeof (CreateExternAccountResponseDto)));
        }

        /// <summary>
        /// Метод генерации тестового сертификата Сгенерированный сертификат не является КЭП. Им можно подписывать документы для отправки документооборота на тестовой площадке или тестовому роботу на боевой.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateCertificateRequest">Параметры для генерации тестового сертификата (optional)</param>
        /// <returns>Task of ApiResponse (CertificateAndPrivateKey)</returns>
        public  Task<ApiResponse<CertificateAndPrivateKey>> GenerateCertificateAsync(GenerateCertificateRequest generateCertificateRequest = null)
        {
            var localPath = "/test-tools/v1/generate-certificate";
            throw new NotImplementedException();
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey);
            //
            // if (generateCertificateRequest != null)
            // {
            //     request.AddJsonBody(generateCertificateRequest);
            // }
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            //
            // var localVarStatusCode = (int) response.StatusCode;
            //
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("GenerateCertificate", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<CertificateAndPrivateKey>(
            //     localVarStatusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (CertificateAndPrivateKey) HttpHelper.Deserialize(response, typeof (CertificateAndPrivateKey)));
        }

        /// <summary>
        /// Генерация входящего письма  Метод обращается к тестовому роботу, который генерирует письмо. Вы увидите входящий документооборот через 20 минут.  Достаточно отправить запрос один раз. Не нужно вызывать метод часто, это нагружает робота и увеличивает время получения письма.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateCuLetterRequest">Данные для генерации письма (optional)</param>
        /// <returns>Task of ApiResponse (Docflow)</returns>
        public  Task<ApiResponse<Docflow>> GenerateCuLetterAsync(GenerateCuLetterRequest generateCuLetterRequest = null)
        {
            var localPath = "/test-tools/v1/generate-cu-letter";
            throw new NotImplementedException();
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey);
            //
            // if (generateCuLetterRequest != null)
            // {
            //     request.AddJsonBody(generateCuLetterRequest);
            // }
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            //
            // var localVarStatusCode = (int) response.StatusCode;
            //
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("GenerateCuLetter", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<Docflow>(
            //     localVarStatusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (Docflow) HttpHelper.Deserialize(response, typeof (Docflow)));
        }

        /// <summary>
        /// Генерация входящего требования Метод обращается к тестовому роботу, который генерирует требование. Вы увидите входящий документооборот через 20 минут.  Достаточно отправить запрос один раз. Не нужно вызывать метод часто, это нагружает робота и увеличивает время получения требования.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateDemandRequest">Данные для генерации входящего требования (optional)</param>
        /// <returns>Task of ApiResponse (Docflow)</returns>
        public  Task<ApiResponse<Docflow>> GenerateDemandAsync(GenerateDemandRequest generateDemandRequest = null)
        {
            var localPath = "/test-tools/v1/generate-demand";
            throw new NotImplementedException();
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey);
            //
            // if (generateDemandRequest != null)
            // {
            //     request.AddJsonBody(generateDemandRequest);
            // }
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            //
            // var localVarStatusCode = (int) response.StatusCode;
            //
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("GenerateDemand", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<Docflow>(
            //     localVarStatusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (Docflow) HttpHelper.Deserialize(response, typeof (Docflow)));
        }

        /// <summary>
        /// Генерация входящего документооборота с требованием и письмом  Метод обращается к тестовому роботу, который генерирует документообороты. Вы увидите входящие документообороты через 20 минут.  Достаточно отправить запрос один раз. Не нужно вызывать метод часто, это нагружает робота и увеличивает время получения документооборота.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateDocflowsSuiteRequest">Данные для генерации входящего документооборота (optional)</param>
        /// <returns>Task of ApiResponse (DocflowsSuite)</returns>
        public  Task<ApiResponse<DocflowsSuite>> GenerateDocflowsSuiteAsync(GenerateDocflowsSuiteRequest generateDocflowsSuiteRequest = null)
        {
            var localPath = "/test-tools/v1/generate-docflows-suite";
            throw new NotImplementedException();
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey);
            //
            // if (generateDocflowsSuiteRequest != null)
            // {
            //     request.AddJsonBody(generateDocflowsSuiteRequest);
            // }
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            //
            // var localVarStatusCode = (int) response.StatusCode;
            //
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("GenerateDocflowsSuite", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<DocflowsSuite>(
            //     localVarStatusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (DocflowsSuite) HttpHelper.Deserialize(response, typeof (DocflowsSuite)));
        }

        /// <summary>
        /// Генерация отчета ФНС (ФУФ ССЧ) Метод создает исходящий документооборот в указанном статусе. Его можно использовать для быстрого создания необходимых документооборотов в учетной записи.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="fnsReportRequest"> (optional)</param>
        /// <returns>Task of ApiResponse (Docflow)</returns>
        public  Task<ApiResponse<Docflow>> GenerateFnsAsync(GenerateFnsReportRequest fnsReportRequest = null)
        {
            var localPath = "/test-tools/v1/generate-fns";
            throw new NotImplementedException();
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey);
            //
            // if (fnsReportRequest != null)
            // {
            //     request.AddJsonBody(fnsReportRequest);
            // }
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            // var localVarStatusCode = (int) response.StatusCode;
            //
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("GenerateFns", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<Docflow>(
            //     localVarStatusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (Docflow) HttpHelper.Deserialize(response, typeof (Docflow)));
        }

        /// <summary>
        /// Генерация ФУФ ЕНВД Единый налог на вмененный доход
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateFufRequest">Данные об организации, которая будет оправлять документ в налоговый орган (optional)</param>
        /// <returns>Task of ApiResponse (string)</returns>
        public  Task<ApiResponse<string>> GenerateFufEnvdAsync(GenerateFufRequest generateFufRequest = null)
        {
            var localPath = "/test-tools/v1/generate-fuf-envd";
            throw new NotImplementedException();
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey);
            //
            // if (generateFufRequest != null)
            // {
            //     request.AddJsonBody(generateFufRequest);
            // }
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            //
            // var localVarStatusCode = (int) response.StatusCode;
            //
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("GenerateFufEnvd", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<string>(
            //     localVarStatusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (string) HttpHelper.Deserialize(response, typeof (string)));
        }

        /// <summary>
        /// Генерация ФУФ НДС с приложениями Фуф НДС с приложениями - книгой покупок и книгой продаж
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateFufRequest">Данные об организации, которая будет оправлять документ в налоговый орган (optional)</param>
        /// <returns>Task of ApiResponse (NdsWithAttachments)</returns>
        public  Task<ApiResponse<NdsWithAttachments>> GenerateFufNdsWithAttachmentsAsync(GenerateFufRequest generateFufRequest = null)
        {
            var localPath = "/test-tools/v1/generate-fuf-nds-with-attachments";
            throw new NotImplementedException();
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey);
            //
            // if (generateFufRequest != null)
            // {
            //     request.AddJsonBody(generateFufRequest);
            // }
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            //
            // var localVarStatusCode = (int) response.StatusCode;
            //
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("GenerateFufNdsWithAttachments", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<NdsWithAttachments>(
            //     localVarStatusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (NdsWithAttachments) HttpHelper.Deserialize(response, typeof (NdsWithAttachments)));
        }

        /// <summary>
        /// Генерация ФУФ налога на прибыль 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateFufRequest">Данные об организации, которая будет оправлять документ в налоговый орган (optional)</param>
        /// <returns>Task of ApiResponse (string)</returns>
        public  Task<ApiResponse<string>> GenerateFufProfitTaxAsync(GenerateFufRequest generateFufRequest = null)
        {
            var localPath = "/test-tools/v1/generate-fuf-profit-tax";
            throw new NotImplementedException();
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey);
            //
            // if (generateFufRequest != null)
            // {
            //     request.AddJsonBody(generateFufRequest);
            // }
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            //
            // var statusCode = (int) response.StatusCode;
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("GenerateFufProfitTax", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<string>(
            //     statusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (string) HttpHelper.Deserialize(response, typeof (string)));
        }

        /// <summary>
        /// Генерация ФУФ ССЧ 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateFufRequest">Данные об организации, которая будет оправлять документ в налоговый орган (optional)</param>
        /// <returns>Task of ApiResponse (string)</returns>
        public  Task<ApiResponse<string>> GenerateFufSschAsync(GenerateFufRequest generateFufRequest = null)
        {
            var localPath = "/test-tools/v1/generate-fuf-ssch";
            throw new NotImplementedException();
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey);
            //
            // if (generateFufRequest != null)
            // {
            //     request.AddJsonBody(generateFufRequest);
            // }
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            //
            // var localVarStatusCode = (int) response.StatusCode;
            //
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("GenerateFufSsch", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<string>(
            //     localVarStatusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (string) HttpHelper.Deserialize(response, typeof (string)));
        }

        /// <summary>
        /// Метод возвращает календарь отчетности в формате XML.
        /// Календарь содержит даты отчетности, названия и коды форм отчетности, GFV, периоды и другую информацию.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (XmlCalendar)</returns>
        public Task<ApiResponse<XmlCalendar>> GenerateXmlCalendarAsync()
        {
            var localPath = "/test-tools/v1/generate-xml-calendar";
            throw new NotImplementedException();
            // var request = new RestRequest(localPath, Method.POST)
            //     .AddHeader("Accept", "application/json")
            //     .AddHeader("X-Kontur-Apikey", Configuration.ApiKey)
            //     .AddJsonBody(new GenerateXmlCalendarRequest());
            //
            // var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);
            //
            // var statusCode = (int) response.StatusCode;
            // if (ExceptionFactory != null)
            // {
            //     var exception = ExceptionFactory("GenerateXmlCalendar", response);
            //     if (exception != null) throw exception;
            // }
            //
            // return new ApiResponse<XmlCalendar>(
            //     statusCode,
            //     response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //     (XmlCalendar) HttpHelper.Deserialize(response, typeof (XmlCalendar)));
        }
    }
}