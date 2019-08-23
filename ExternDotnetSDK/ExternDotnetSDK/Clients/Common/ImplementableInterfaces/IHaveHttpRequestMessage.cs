using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ExternDotnetSDK.Clients.Common.ImplementableInterfaces
{
    /// <summary>
    ///     Implements a request sent to server, like HttpRequestMessage class.
    /// </summary>
    public interface IHaveHttpRequestMessage
    {
        HttpRequestHeaders Headers { get; }
        HttpContent Content { get; }
        HttpMethod Method { get; }
        Uri Uri { get; }
    }
}