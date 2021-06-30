using System;

namespace Kontur.Extern.Client.HttpLevel
{
    public static class HttpRequestsFactoryVerbExtension
    {
        public static IHttpRequest Get(this IHttpRequestsFactory requestsFactory, string url) => requestsFactory.Get(new Uri(url));

        public static IPayloadHttpRequest Put(this IHttpRequestsFactory requestsFactory, string url) => requestsFactory.Put(new Uri(url));

        public static IPayloadHttpRequest Post(this IHttpRequestsFactory requestsFactory, string url) => requestsFactory.Post(new Uri(url));

        public static IHttpRequest Delete(this IHttpRequestsFactory requestsFactory, string url) => requestsFactory.Delete(new Uri(url));
    }
}