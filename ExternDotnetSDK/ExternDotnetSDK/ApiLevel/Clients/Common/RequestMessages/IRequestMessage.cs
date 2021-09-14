using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Common.RequestMessages
{
    public interface IRequestMessage
    {
        Dictionary<string, string> Headers { get; }
        HttpContent Content { get; }
        HttpMethod Method { get; }
        Uri Uri { get; }
    }
}