using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests;
using Kontur.Extern.Client.Http.Contents;
using Kontur.Extern.Client.Http.Models;

namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models
{
    internal static class FormUrlEncodedContentExtension
    {
        public static FormUrlEncodedContent AddGrantType(this FormUrlEncodedContent content, in UrlEncodedString grantType) => 
            content.AddEntry(ContractConstants.TokenRequest.GrantType, grantType);

        public static FormUrlEncodedContent AddScope(this FormUrlEncodedContent content, in UrlEncodedString scope) => 
            content.AddEntryIfNotEmpty(ContractConstants.TokenRequest.Scope, scope);

        public static FormUrlEncodedContent AddRequestAuthentication(this FormUrlEncodedContent content, ClientAuthenticatedRequest tokenRequest) => 
            content.AddClientId(tokenRequest.ClientId).AddClientSecret(tokenRequest.ClientSecret);

        public static FormUrlEncodedContent AddClientId(this FormUrlEncodedContent content, in UrlEncodedString clientId) => 
            content.AddEntry(ContractConstants.ClientAuthenticate.ClientId, clientId);
        
        public static FormUrlEncodedContent AddClientSecret(this FormUrlEncodedContent content, in UrlEncodedString clientId) => 
            content.AddEntry(ContractConstants.ClientAuthenticate.ClientSecret, clientId);
    }
}