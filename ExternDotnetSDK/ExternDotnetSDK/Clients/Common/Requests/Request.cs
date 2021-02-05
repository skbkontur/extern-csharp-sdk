using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Clients.Common.Requests
{
    public class Request
    {
        public Request(
            RequestMethod method,
            Uri url,
            [CanBeNull] Dictionary<string, string> headers = null)
            : this(method, url, null, null, null, headers)
        {
        }

        public Request(
            RequestMethod method,
            Uri url,
            [CanBeNull] byte[] content,
            [CanBeNull] Dictionary<string, string> headers = null)
            : this(method, url, content, null, null, headers)
        {
        }

        public Request(
            RequestMethod method,
            Uri url,
            [CanBeNull] Stream streamContent,
            [CanBeNull] Dictionary<string, string> headers = null)
            : this(method, url, null, streamContent, null, headers)
        {
        }

        public Request(
            RequestMethod method,
            Uri url,
            [CanBeNull] string jsonContent,
            [CanBeNull] Dictionary<string, string> headers = null)
            : this(method, url, null, null, jsonContent, headers)
        {
        }

        private Request(
            RequestMethod method,
            Uri url,
            [CanBeNull] byte[] content,
            [CanBeNull] Stream streamContent,
            [CanBeNull] string jsonContent,
            [CanBeNull] Dictionary<string, string> headers)
        {
            Method = method;
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Content = content;
            StreamContent = streamContent;
            JsonContent = jsonContent;
            Headers = headers;
        }

        public RequestMethod Method { get; }

        public Uri Url { get; private set; }

        [CanBeNull]
        public byte[] Content { get; private set; }

        [CanBeNull]
        public Stream StreamContent { get; private set; }

        [CanBeNull]
        public string JsonContent { get; private set; }

        [CanBeNull]
        public Dictionary<string, string> Headers { get; private set; }

        public Request WithUrl(string url)
        {
            return WithUrl(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        public Request WithUrl(Uri url)
        {
            Url = url;
            return this;
        }

        public Request WithContent(byte[] content)
        {
            Content = content;
            return WithHeader(HeaderNames.ContentLength, content.Length);
        }

        public Request WithContent(Stream streamContent)
        {
            StreamContent = streamContent;
            return this;
        }

        public Request WithContent(string jsonContent)
        {
            JsonContent = jsonContent;
            return this;
        }

        public Request WithHeader<T>(string name, T value)
        {
            return WithHeader(name, value.ToString());
        }

        public Request WithHeader(string name, string value)
        {
            Headers = Headers ?? new Dictionary<string, string>();
            Headers[name] = value;
            return this;
        }

        public Request WithHeaders(Dictionary<string, string> headers)
        {
            Headers = headers;
            return this;
        }

        public override string ToString()
        {
            return ToString(true, true);
        }

        public string ToString(bool includeQuery, bool includeHeaders)
        {
            var builder = new StringBuilder();

            builder.Append(Method);
            builder.Append(" ");

            var urlString = Url.ToString();

            if (!includeQuery)
            {
                var queryBeginning = urlString.IndexOf("?", StringComparison.Ordinal);
                if (queryBeginning >= 0)
                    urlString = urlString.Substring(0, queryBeginning);
            }

            builder.Append(urlString);

            if (includeHeaders && Headers != null && Headers.Count > 0)
            {
                foreach (var header in Headers)
                {
                    builder.AppendLine();

                    builder.Append(header.Key);
                    builder.Append(": ");
                    builder.Append(header.Value);
                }
            }

            return builder.ToString();
        }

        #region Factory methods

        public static Request Get(Uri url)
        {
            return new Request(RequestMethod.Get, url);
        }

        public static Request Get(string url)
        {
            return Get(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        public static Request Post(Uri url)
        {
            return new Request(RequestMethod.Post, url);
        }

        public static Request Post(string url)
        {
            return Post(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        public static Request Put(Uri url)
        {
            return new Request(RequestMethod.Put, url);
        }

        public static Request Put(string url)
        {
            return Put(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        public static Request Delete([NotNull] Uri url)
        {
            return new Request(RequestMethod.Delete, url);
        }

        public static Request Delete([NotNull] string url)
        {
            return Delete(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        #endregion
    }
}