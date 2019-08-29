using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using KeApiClientOpenSdk.Clients.Common.RequestMessages;

namespace KeApiClientOpenSdk.Clients.Common.ResponseMessages
{
    public interface IResponseMessage
    {
        HttpContent Content { get; }
        Dictionary<string, string> Headers { get; }
        HttpStatusCode StatusCode { get; }
        IRequestMessage Request { get; }
        string ReasonPhrase { get; }
        IResponseMessage EnsureSuccessStatusCode();
    }
}