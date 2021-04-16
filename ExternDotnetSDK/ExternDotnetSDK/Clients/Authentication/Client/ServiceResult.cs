using System;
using System.Collections.Generic;
using System.Text;

namespace Kontur.Extern.Client.Clients.Authentication.Client
{
    public class ServiceResult<TError> : ServiceResult
        where TError : class
    {
        internal ServiceResult(TError serviceError, ServiceResultStatus status, int? serviceResponseCode = null, string errorMessage = null)
            : base(status, serviceResponseCode, errorMessage)
        {
            ServiceError = serviceError;
        }

        /// <summary>
        /// Информация об ошибке, полученная от Identity-провайдера
        /// </summary>
        public TError ServiceError { get; }

        public static ServiceResult<TError> CreateSuccessful(int? responseCode = 200) =>
            new ServiceResult<TError>(null, ServiceResultStatus.Success, responseCode);

        public static ServiceResult<TError> CreateServiceError(TError serviceError, int responseCode, string message) =>
            new ServiceResult<TError>(serviceError, ServiceResultStatus.ServiceError, responseCode, message);

        public new static ServiceResult<TError> CreateServiceUnavailable(string message) =>
            new ServiceResult<TError>(null, ServiceResultStatus.ServiceUnavailable, errorMessage: message);

        public new static ServiceResult<TError> CreateUnknownError(string message) =>
            new ServiceResult<TError>(null, ServiceResultStatus.UnknownError, errorMessage: message);
    }
    public class ServiceResult
    {
        internal ServiceResult(ServiceResultStatus status, int? serviceResponseCode = null, string errorMessage = null)
        {
            Status = status;
            ServiceResponseCode = serviceResponseCode;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Статус операции. Успешный результат - <see cref="ServiceResultStatus.Success"/>
        /// </summary>
        public ServiceResultStatus Status { get; }

        /// <summary>
        /// Код http-ответа, полученного от сервиса. Может отсутствовать, если ответ не был получен (возвращается <see langword="null"/>).
        /// </summary>
        public int? ServiceResponseCode { get; }

        public string ErrorMessage { get; }

        public static ServiceResult CreateSuccessful(int responseCode) =>
            new ServiceResult(ServiceResultStatus.Success, responseCode);

        public static ServiceResult CreateServiceError(int responseCode, string message) =>
            new ServiceResult(ServiceResultStatus.ServiceError, responseCode, message);

        public static ServiceResult CreateServiceUnavailable(string message) =>
            new ServiceResult(ServiceResultStatus.ServiceUnavailable, errorMessage: message);

        public static ServiceResult CreateUnknownError(string message) =>
            new ServiceResult(ServiceResultStatus.UnknownError, errorMessage: message);
    }

    public class ServiceResult<TResponse, TError> : ServiceResult<TError>
        where TResponse : class
        where TError : class
    {
        internal ServiceResult(TError serviceError, TResponse response, ServiceResultStatus status, int? serviceResponseCode = null, string errorMessage = null)
            : base(serviceError, status, serviceResponseCode, errorMessage)
        {
            Response = response;
        }

        /// <summary>
        /// Информация об ошибке, полученная от Identity-провайдера
        /// </summary>
        public TResponse Response { get; }

        public static ServiceResult<TResponse, TError> CreateSuccessful(TResponse response, int? responseCode = 200) =>
            new ServiceResult<TResponse, TError>(null, response, ServiceResultStatus.Success, responseCode);

        public new static ServiceResult<TResponse, TError> CreateServiceError(TError error, int responseCode, string message) =>
            new ServiceResult<TResponse, TError>(error, null, ServiceResultStatus.ServiceError, responseCode, message);

        public new static ServiceResult<TResponse, TError> CreateServiceUnavailable(string message) =>
            new ServiceResult<TResponse, TError>(null, null, ServiceResultStatus.ServiceUnavailable, errorMessage: message);

        public new static ServiceResult<TResponse, TError> CreateUnknownError(string message) =>
            new ServiceResult<TResponse, TError>(null, null, ServiceResultStatus.UnknownError, errorMessage: message);
    }
    public enum ServiceResultStatus
    {
        Success,

        /// <summary>
        /// Не удалось получить ответ от сервиса за отведенное время.
        /// </summary>
        ServiceUnavailable,

        /// <summary>
        /// От сервиса был получен ответ, сигнализирующий об ошибке.
        /// </summary>
        ServiceError,

        /// <summary>
        /// Во время выполнения операции произошла ошибка неизвестной природы.
        /// </summary>
        UnknownError
    }
}
