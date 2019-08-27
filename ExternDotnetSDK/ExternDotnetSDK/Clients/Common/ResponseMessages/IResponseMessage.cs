using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using KeApiOpenSdk.Clients.Common.RequestMessages;

namespace KeApiOpenSdk.Clients.Common.ResponseMessages
{
    public interface IResponseMessage
    {
        HttpContent Content { get; }
        HttpResponseHeaders Headers { get; }
        HttpStatusCode StatusCode { get; }
        IRequestMessage Request { get; }
        string ReasonPhrase { get; }
        IResponseMessage EnsureSuccessStatusCode();
    }
}