using System.IO;
using FluentAssertions;
using FluentAssertions.Execution;
using JetBrains.Annotations;
using Kontur.Extern.Client.Auth.OpenId.Provider.Models;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment
{
    internal class AuthTestData
    {
        private const string DefaultOpenIdServer = "https://identity.testkontur.ru";
        
        public static AuthTestData LoadFromJsonFile()
        {
            var json = File.ReadAllText("auth_params.json");
            return JsonConvert.DeserializeObject<AuthTestData>(json);
        }

        public AuthTestData(string userName, string password, string scope, string apiKey, string clientId, string? openIdServer = DefaultOpenIdServer)
        {
            openIdServer ??= DefaultOpenIdServer;
            ShouldNotBeNullOrEmpty(userName, nameof(userName));
            ShouldNotBeNullOrEmpty(password, nameof(password));
            ShouldNotBeNullOrEmpty(scope, nameof(scope));
            ShouldNotBeNullOrEmpty(apiKey, nameof(apiKey));
            ShouldNotBeNullOrEmpty(clientId, nameof(clientId));
            ShouldNotBeNullOrEmpty(openIdServer, nameof(openIdServer));
            
            UserName = userName;
            Password = password;
            Scope = scope;
            ApiKey = apiKey;
            ClientId = clientId;
            OpenIdServer = openIdServer;
        }

        public string UserName { get; }
        public string Password { get; }
        public Credentials UserCredentials => new(UserName, Password);
        public string Scope { get; }
        public string ApiKey { get; }
        public string ClientId { get; }
        public string OpenIdServer { get; }

        private static void ShouldNotBeNullOrEmpty(string value, [InvokerParameterName] string paramName)
        {
            using var scope = new AssertionScope(paramName);
            value.Should().NotBeNullOrWhiteSpace();
        }
    }
}