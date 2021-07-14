using System;
using System.Text;

namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models
{
    internal class FormUrlEncodedContentBuilder
    {
        private readonly StringBuilder stringBuilder = new();

        public FormUrlEncodedContentBuilder Add(string name, string value)
        {
            if (stringBuilder.Length > 0)
                stringBuilder.Append('&');

            stringBuilder
                .Append(Encode(name))
                .Append('=')
                .Append(Encode(value));

            return this;
        }

        public FormUrlEncodedContentBuilder AddIfNotNull(string name, string value)
            => value != null ? Add(name, value) : this;

        public FormUrlEncodedContentBuilder AddGrantType(string grantType)
        {
            return Add(ContractConstants.TokenRequest.GrantType, grantType);
        }

        public FormUrlEncodedContentBuilder AddScope(string scope)
        {
            return AddIfNotNull(ContractConstants.TokenRequest.Scope, scope);
        }

        public override string ToString() => stringBuilder.ToString();

        private static string Encode(string data)
        {
            if (string.IsNullOrEmpty(data))
                return string.Empty;
            return Uri.EscapeDataString(data).Replace("%20", "+");
        }
    }
}