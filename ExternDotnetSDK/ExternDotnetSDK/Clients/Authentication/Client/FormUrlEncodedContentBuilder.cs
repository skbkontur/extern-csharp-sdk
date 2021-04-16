using System;
using System.Text;
using Kontur.Extern.Client.Clients.Authentication.Client.Models;

namespace Kontur.Extern.Client.Clients.Authentication.Client
{
    internal class FormUrlEncodedContentBuilder
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

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
            return Add(ClientConstants.TokenRequest.GrantType, grantType);
        }

        public FormUrlEncodedContentBuilder AddScope(string scope)
        {
            return AddIfNotNull(ClientConstants.TokenRequest.Scope, scope);
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