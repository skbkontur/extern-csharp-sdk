using Kontur.Extern.Client.HttpLevel.Contents;

namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models
{
    internal static class FormUrlEncodedContentExtension
    {
        public static FormUrlEncodedContent AddGrantType(this FormUrlEncodedContent content, string grantType) => 
            content.AddEntry(ContractConstants.TokenRequest.GrantType, grantType);

        public static FormUrlEncodedContent AddScope(this FormUrlEncodedContent content, string scope) => 
            content.AddEntryIfNotEmpty(ContractConstants.TokenRequest.Scope, scope);
    }
}