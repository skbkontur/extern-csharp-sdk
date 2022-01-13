namespace Kontur.Extern.Api.Client.Http
{
    public static class HttpRequestFactoryVerbExtension
    {
        public static IHttpRequest Get(this IHttpRequestFactory requestFactory, string url) => requestFactory.Get(url.ToUrl());

        public static IPayloadHttpRequest Put(this IHttpRequestFactory requestFactory, string url) => requestFactory.Put(url.ToUrl());

        public static IPayloadHttpRequest Post(this IHttpRequestFactory requestFactory, string url) => requestFactory.Post(url.ToUrl());

        public static IHttpRequest Delete(this IHttpRequestFactory requestFactory, string url) => requestFactory.Delete(url.ToUrl());
    }
}