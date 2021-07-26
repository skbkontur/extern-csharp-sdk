using System;

namespace Kontur.Extern.Client.Http
{
    public interface IHttpRequestsFactory
    {
        IHttpRequest Get(Uri url);
        IPayloadHttpRequest Put(Uri url);
        IPayloadHttpRequest Post(Uri url);
        IHttpRequest Delete(Uri url);
    }
}