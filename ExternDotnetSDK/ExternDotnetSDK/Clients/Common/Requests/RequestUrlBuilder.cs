using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Clients.Common.Requests
{
    public class RequestUrlBuilder
    {
        private readonly StringBuilder builder;
        private bool hasQueryParameters;

        public RequestUrlBuilder()
        {
            builder = new StringBuilder();
        }

        public RequestUrlBuilder(string initialUrl = "")
        {
            if (initialUrl == null)
                throw new ArgumentNullException(nameof(initialUrl));

            builder = new StringBuilder(initialUrl);
            hasQueryParameters = initialUrl.IndexOf("?", StringComparison.Ordinal) >= 0;
        }

        public Uri Build()
        {
            return new Uri(builder.ToString(), UriKind.RelativeOrAbsolute);
        }

        public RequestUrlBuilder AppendToQuery<T>(string key, [CanBeNull] T value)
        {
            return AppendToQuery(key, value?.ToString());
        }

        public RequestUrlBuilder AppendToQuery<T>(string key, IEnumerable<T> values)
        {
            foreach (var value in values)
                AppendToQuery(key, value?.ToString());
            return this;
        }

        public RequestUrlBuilder AppendToQuery(string key, [CanBeNull] string value)
        {
            if (string.IsNullOrEmpty(value))
                return this;

            if (hasQueryParameters)
            {
                builder.Append('&');
            }
            else
            {
                builder.Append('?');
                hasQueryParameters = true;
            }

            builder.Append(key);
            builder.Append('=');
            builder.Append(value);

            return this;
        }
    }
}