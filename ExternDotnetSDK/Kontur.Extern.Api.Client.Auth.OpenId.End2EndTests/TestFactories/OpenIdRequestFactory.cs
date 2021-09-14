using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests;
using Kontur.Extern.Api.Client.Auth.OpenId.Provider.Models;
using Kontur.Extern.Api.Client.Testing.End2End.Environment;

namespace Kontur.Extern.Api.Client.Auth.OpenId.End2EndTests.TestFactories
{
    internal class OpenIdRequestFactory
    {
        private readonly AuthTestData testData;

        public OpenIdRequestFactory(AuthTestData testData) => this.testData = testData;

        public PasswordTokenRequest CorrectPasswordAuthenticationRequest() => 
            new(testData.UserCredentials, testData.Scope, testData.ClientId, testData.ApiKey);

        public PasswordTokenRequest PasswordAuthenticationRequestWithIncorrectScope() => 
            new(testData.UserCredentials, "unknown scope", testData.ClientId, testData.ApiKey);
        
        public PasswordTokenRequest PasswordAuthenticationRequestWithIncorrectClientId() => 
            new(testData.UserCredentials, testData.Scope, "unknown client", testData.ApiKey);
        
        public PasswordTokenRequest PasswordAuthenticationRequestWithIncorrectApiKey() => 
            new(testData.UserCredentials, testData.Scope, testData.ClientId, "some unknown");
        
        public PasswordTokenRequest PasswordAuthenticationRequestWithIncorrectCredentials() =>
            new(new Credentials("unknown username", "unknown password"), testData.Scope, testData.ClientId, testData.ApiKey);
    }
}