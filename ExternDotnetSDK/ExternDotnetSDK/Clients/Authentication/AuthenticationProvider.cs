using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KeApiOpenSdk.Models.Authentication;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Clients.Authentication
{
    //todo Сделать нормальные тесты для методов.
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private const string MediaType = "application/json";
        private const string TimeoutHeader = "Timeout";

        private readonly string login;
        private readonly string password;
        private readonly string authenticationBaseAddress;
        private readonly HttpClient client;

        public AuthenticationProvider(string login, string password, string authenticationBaseAddress)
        {
            this.login = login;
            this.password = password;
            this.authenticationBaseAddress = authenticationBaseAddress;
            client = new HttpClient();
        }

        public async Task<string> GetSessionId(TimeSpan? timeout = null)
        {
            var fullUrl = $"{authenticationBaseAddress}/v5.13/authenticate-by-pass?login={login}";
            var request = new HttpRequestMessage(HttpMethod.Post, fullUrl)
            {
                Content = new StringContent(password, Encoding.UTF8, MediaType)
            };
            if (timeout != null)
                request.Headers.Add(TimeoutHeader, timeout.Value.ToString("c"));
            var response = await client.SendAsync(request);
            var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await response.Content.ReadAsStringAsync());
            return authResponse.Sid;
        }

        public async Task<PasswordStrength> GetPasswordStrength(TimeSpan? timeout = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{authenticationBaseAddress}/v5.13/get-pass-strength")
            {
                Content = new StringContent(password, Encoding.UTF8, MediaType)
            };
            if (timeout != null)
                request.Headers.Add(TimeoutHeader, timeout.Value.ToString("c"));
            var response = await client.SendAsync(request);
            return JsonConvert.DeserializeObject<PasswordStrength>(await response.Content.ReadAsStringAsync());
        }
    }
}