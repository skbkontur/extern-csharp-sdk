using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using JetBrains.Annotations;
using Kontur.Extern.Client.Clients.Common.RequestSenders;

namespace Kontur.Extern.Client.Clients.Authentication.Client.Extensions
{
    internal static class HttpRequestMessageContentExtension
    {
        [NotNull]
        public static HttpRequestMessage WithContent([NotNull] this HttpRequestMessage request, [NotNull] string content, string mediaType)
        {
            request.Content = new StringContent(content, Encoding.UTF8, mediaType);
            return request;
        }

        [NotNull]
        public static HttpRequestMessage WithContent([NotNull] this HttpRequestMessage request, [NotNull] Stream stream)
        {
            request.Content = new StreamContent(stream);
            return request;
        }
    }

    internal static class HttpRequestMessageExtension
    {
        public static HttpRequestMessage TrySetTimeoutHeader(this HttpRequestMessage request, TimeSpan? timeout)
        {
            if (timeout != null)
                request.Headers.Add(SenderConstants.TimeoutHeader, timeout.Value.ToString("c"));
            return request;
        }

        public static HttpRequestMessage WithAcceptHeader(this HttpRequestMessage message, string value)
        {
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(value));
            return message;
        }

        [NotNull]
        public static HttpRequestMessage WithAcceptCharsetHeader([NotNull] this HttpRequestMessage request, [NotNull] string value)
        {
            request.Headers.AcceptCharset.Add(new StringWithQualityHeaderValue(value));
            return request;
        }

        [NotNull]
        public static HttpRequestMessage WithContentTypeHeader([NotNull] this HttpRequestMessage request, [NotNull] string value)
        {
            return request.WithHeader(HeaderNames.ContentType, value);
        }

        [NotNull]
        public static HttpRequestMessage WithAcceptCharsetHeader([NotNull] this HttpRequestMessage request, [NotNull] Encoding value)
        {
            return WithAcceptCharsetHeader(request, value.WebName);
        }

        [NotNull]
        public static HttpRequestMessage WithAcceptEncodingHeader([NotNull] this HttpRequestMessage request, [NotNull] string value)
        {
            return request.WithHeader(HeaderNames.AcceptEncoding, value);
        }

        [NotNull]
        public static HttpRequestMessage WithAuthorizationHeader([NotNull] this HttpRequestMessage request, [NotNull] string value)
        {
             return request.WithHeader(HeaderNames.Authorization, value);
        }

        [NotNull]
        public static HttpRequestMessage WithKonturApiKeyHeader([NotNull] this HttpRequestMessage request, [NotNull] string apiKey)
        {
            return WithHeader(request, HeaderNames.ApiKeyHeader, apiKey);
        }

        [NotNull]
        public static HttpRequestMessage WithBasicAuthorizationHeader([NotNull] this HttpRequestMessage request, [NotNull] string user, [NotNull] string password)
        {
            return WithAuthorizationHeader(request, "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(user + ":" + password)));
        }

        [Pure]
        [NotNull]
        public static HttpRequestMessage WithHeader([NotNull] this HttpRequestMessage request, [NotNull] string name, [NotNull] string value)
        {
            if (!string.IsNullOrEmpty(value))
                request.Headers.Add(name, value);
            return request;
        }
    }

    [PublicAPI]
    public static class HeaderNames
    {
        public const string Authorization = "Authorization";
        public const string AcceptEncoding = "Accept-Encoding";
        public const string ContentType = "Content-Type";
        public const string RequestTimeout = "Request-Timeout";
        public const string ApiKeyHeader = "X-Kontur-Apikey";
    }
}