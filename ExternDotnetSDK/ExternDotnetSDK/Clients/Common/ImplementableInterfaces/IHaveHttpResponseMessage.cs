using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ExternDotnetSDK.Clients.Common.ImplementableInterfaces
{
    /// <summary>
    ///     Implements a response received from server, like HttpResponseMessage class.
    /// </summary>
    public interface IHaveHttpResponseMessage
    {
        HttpContent Content { get; }
        HttpResponseHeaders Headers { get; }
        HttpStatusCode StatusCode { get; }
        IHaveHttpRequestMessage Request { get; }
        string ReasonPhrase { get; }
        IHaveHttpResponseMessage EnsureSuccessStatusCode();
    }
}