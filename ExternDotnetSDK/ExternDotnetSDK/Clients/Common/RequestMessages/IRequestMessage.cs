using System;
using System.Collections.Generic;
using System.Net.Http;

namespace KeApiClientOpenSdk.Clients.Common.RequestMessages
{
    public interface IRequestMessage
    {
        Dictionary<string, string> Headers { get; }
        HttpContent Content { get; }
        HttpMethod Method { get; }
        Uri Uri { get; }
    }
}