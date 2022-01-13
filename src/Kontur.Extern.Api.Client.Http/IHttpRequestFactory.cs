using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Http
{
    [PublicAPI]
    public interface IHttpRequestFactory
    {
        IHttpRequest Get(Uri url);
        IPayloadHttpRequest Put(Uri url);
        IPayloadHttpRequest Post(Uri url);
        IHttpRequest Delete(Uri url);
        IPayloadHttpRequest Verb(string method, Uri url);
    }
}