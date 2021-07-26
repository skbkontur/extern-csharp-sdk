namespace Kontur.Extern.Client.Http
{
    public static class HttpRequestsFactoryVerbExtension
    {
        public static IHttpRequest Get(this IHttpRequestsFactory requestsFactory, string url) => requestsFactory.Get(url.ToUrl());

        public static IPayloadHttpRequest Put(this IHttpRequestsFactory requestsFactory, string url) => requestsFactory.Put(url.ToUrl());

        public static IPayloadHttpRequest Post(this IHttpRequestsFactory requestsFactory, string url) => requestsFactory.Post(url.ToUrl());

        public static IHttpRequest Delete(this IHttpRequestsFactory requestsFactory, string url) => requestsFactory.Delete(url.ToUrl());
    }
}