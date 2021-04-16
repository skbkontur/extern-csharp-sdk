using System.Net.Http;
using Kontur.Extern.Client.Clients.Authentication.Client.Factories;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Clients.Authentication.Client
{
    internal class ServiceResultFactory<TResponse, TError> : IResultFactory<ServiceResult<TResponse, TError>>
        where TResponse : class
        where TError : class
    {
        private readonly ServiceErrorBuilder serviceErrorBuilder;

        private ServiceResultFactory(ServiceErrorBuilder serviceErrorBuilder)
        {
            this.serviceErrorBuilder = serviceErrorBuilder;
        }

        public ServiceResult<TResponse, TError> CreateSuccessful(HttpResponseMessage response)
        {
            return ServiceResult<TResponse, TError>.CreateSuccessful(
                JsonConvert.DeserializeObject<TResponse>(response.Content.ToString()),
                (int) response.StatusCode);
        }

        public ServiceResult<TResponse, TError> CreateServiceError(HttpResponseMessage response)
        {
            return ServiceResult<TResponse, TError>.CreateServiceError(
                serviceErrorBuilder.BuildError<TError>(response.Content),
                (int) response.StatusCode,
                response.StatusCode.ToString());
        }

        public ServiceResult<TResponse, TError> CreateServiceUnavailable(string message)
        {
            return ServiceResult<TResponse, TError>.CreateServiceUnavailable(message);
        }

        public ServiceResult<TResponse, TError> CreateUnknownError(string message)
        {
            return ServiceResult<TResponse, TError>.CreateUnknownError(message);
        }

        public static readonly ServiceResultFactory<TResponse, TError> Instance = new ServiceResultFactory<TResponse, TError>(
            new ServiceErrorBuilder());
    }

    internal class ServiceResultFactory<TError> : IResultFactory<ServiceResult<TError>>
        where TError : class
    {
        private readonly ServiceErrorBuilder serviceErrorBuilder;

        private ServiceResultFactory(ServiceErrorBuilder serviceErrorBuilder)
        {
            this.serviceErrorBuilder = serviceErrorBuilder;
        }

        public ServiceResult<TError> CreateSuccessful(HttpResponseMessage response)
        {
            return ServiceResult<TError>.CreateSuccessful((int) response.StatusCode);
        }

        public ServiceResult<TError> CreateServiceError(HttpResponseMessage response)
        {
            return ServiceResult<TError>.CreateServiceError(
                serviceErrorBuilder.BuildError<TError>(response.Content),
                (int) response.StatusCode,
                response.StatusCode.ToString());
        }

        public ServiceResult<TError> CreateServiceUnavailable(string message)
        {
            return ServiceResult<TError>.CreateServiceUnavailable(message);
        }

        public ServiceResult<TError> CreateUnknownError(string message)
        {
            return ServiceResult<TError>.CreateUnknownError(message);
        }

        public static readonly ServiceResultFactory<TError> Instance = new ServiceResultFactory<TError>(new ServiceErrorBuilder());
    }

    internal class ServiceResultFactory : IResultFactory<ServiceResult>
    {
        public ServiceResult CreateSuccessful(HttpResponseMessage response)
        {
            return ServiceResult.CreateSuccessful((int) response.StatusCode);
        }

        public ServiceResult CreateServiceError(HttpResponseMessage response)
        {
            return ServiceResult.CreateServiceError((int) response.StatusCode, response.StatusCode.ToString());
        }

        public ServiceResult CreateServiceUnavailable(string message)
        {
            return ServiceResult.CreateServiceUnavailable(message);
        }

        public ServiceResult CreateUnknownError(string message)
        {
            return ServiceResult.CreateUnknownError(message);
        }

        public static readonly ServiceResultFactory Instance = new ServiceResultFactory();
    }
}