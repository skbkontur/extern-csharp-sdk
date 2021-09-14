using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Common.RequestMessages;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Common.ResponseMessages
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