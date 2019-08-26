using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ExternDotnetSDK.Clients.Common.RequestMessages
{
    public interface IRequestMessage
    {
        HttpRequestHeaders Headers { get; }
        HttpContent Content { get; }
        HttpMethod Method { get; }
        Uri Uri { get; }
    }
}