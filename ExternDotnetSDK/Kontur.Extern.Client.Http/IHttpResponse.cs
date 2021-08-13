using System;
using System.IO;
using System.Net.Http.Headers;
using JetBrains.Annotations;
using Kontur.Extern.Client.Http.Models.Headers;

namespace Kontur.Extern.Client.Http
{
    [PublicAPI]
    public interface IHttpResponse
    {
        HttpStatus Status { get; }
        bool HasPayload { get; }
        ContentRangeHeaderValue ContentRange { get; }
        ContentType ContentType { get; }

        byte[] GetBytes();
        ArraySegment<byte> GetBytesSegment();
        Stream GetStream();
        string GetString();
        TResponseMessage GetMessage<TResponseMessage>();
        bool TryGetMessage<TResponseMessage>(out TResponseMessage responseMessage);
    }
}