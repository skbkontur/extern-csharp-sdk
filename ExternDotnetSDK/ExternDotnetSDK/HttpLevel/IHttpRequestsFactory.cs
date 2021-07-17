using System;

namespace Kontur.Extern.Client.HttpLevel
{
    public interface IHttpRequestsFactory
    {
        IHttpRequest Get(Uri url);
        IPayloadHttpRequest Put(Uri url);
        IPayloadHttpRequest Post(Uri url);
        IHttpRequest Delete(Uri url);
    }
}