using System.Net.Http;

namespace Kontur.Extern.Client.Clients.Authentication.Client.Factories
{
    internal interface IResultFactory<out TResult>
        where TResult : ServiceResult
    {
        TResult CreateSuccessful(HttpResponseMessage response);
        TResult CreateServiceError(HttpResponseMessage response);
        TResult CreateServiceUnavailable(string message);
        TResult CreateUnknownError(string message);
    }
}