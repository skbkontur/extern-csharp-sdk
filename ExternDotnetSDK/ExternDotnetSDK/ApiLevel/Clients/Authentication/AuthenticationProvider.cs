using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Clients.Common.Logging;
using Kontur.Extern.Client.ApiLevel.Clients.Common.RequestSenders;
using Kontur.Extern.Client.ApiLevel.Clients.Common.ResponseMessages;
using Kontur.Extern.Client.ApiLevel.Models.Authentication;
using Newtonsoft.Json;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Clients.Authentication
{
    //todo Сделать нормальные тесты для методов.
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly ILogger logger;
        private readonly string login;
        private readonly string authenticationBaseAddress;
        private readonly HttpClient client;
        private readonly StringContent content;

        public AuthenticationProvider(string login, string password, string authenticationBaseAddress, ILogger logger = null)
        {
            this.login = login;
            this.authenticationBaseAddress = authenticationBaseAddress;
            this.logger = logger ?? new SilentLogger();
            client = new HttpClient();
            content = new StringContent(password, Encoding.UTF8, SenderConstants.MediaType);
        }

        public async Task<string> GetSessionId(TimeSpan? timeout = null)
        {
            var fullUrl = $"{authenticationBaseAddress}/v5.13/authenticate-by-pass?login={login}";
            var request = new HttpRequestMessage(HttpMethod.Post, fullUrl) {Content = content};
            TrySetTimeoutHeader(timeout, request);
            var response = new ResponseMessage(await client.SendAsync(request).ConfigureAwait(false));
            var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await response.TryGetResponseAsync(logger).ConfigureAwait(false));
            return authResponse.Sid;
        }

        public async Task<PasswordStrength> GetPasswordStrength(TimeSpan? timeout = null)
        {
            var fullUrl = $"{authenticationBaseAddress}/v5.13/get-pass-strength";
            var request = new HttpRequestMessage(HttpMethod.Post, fullUrl) {Content = content};
            TrySetTimeoutHeader(timeout, request);
            var responseMessage = new ResponseMessage(await client.SendAsync(request).ConfigureAwait(false));
            var result = await responseMessage.TryGetResponseAsync(logger).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<PasswordStrength>(result);
        }

        private static void TrySetTimeoutHeader(TimeSpan? timeout, HttpRequestMessage request)
        {
            if (timeout != null)
                request.Headers.Add(SenderConstants.TimeoutHeader, timeout.Value.ToString("c"));
        }
    }
}