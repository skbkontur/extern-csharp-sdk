using Kontur.Extern.Client.Clients.Authentication;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.API
{
    public class Configuration
    {
        public static readonly ExceptionFactory DefaultExceptionFactory = (methodName, response) =>
        {
            var status = (int) response.StatusCode;
            if (status >= 400)
            {
                return new ApiException(
                    status,
                    $"Error calling {methodName}: {response.Content}",
                    response.Content);
            }

            if (status == 0)
            {
                return new ApiException(
                    status,
                    $"Error calling {methodName}: {response.ErrorMessage}",
                    response.ErrorMessage);
            }

            return null;
        };

        public IAuthenticationProvider AuthenticationProvider;

        public Configuration(string basePath, string apiKey, string password, string login)
        {
            UserAgent = "C# SDK";
            BasePath = basePath;
            ApiKey = apiKey;
            Timeout = 100000;
            AuthenticationProvider = new AuthenticationProvider(login, password, "https://api.testkontur.ru/auth");
        }

        public string BasePath { get; set; }

        public int Timeout { get; set; }

        public string UserAgent { get; set; }

        public string AccessToken { get; set; }

        public string ApiKey { get; set; }
    }
}