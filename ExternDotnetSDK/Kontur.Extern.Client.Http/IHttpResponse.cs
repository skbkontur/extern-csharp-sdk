using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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

        ValueTask<byte[]> GetBytesAsync();
        ValueTask<ArraySegment<byte>> GetBytesSegmentAsync();
        Stream GetStream();
        ValueTask<string> GetStringAsync();
        ValueTask<TResponseMessage> GetMessageAsync<TResponseMessage>();
        ValueTask<TResponseMessage?> TryGetMessageAsync<TResponseMessage>();
    }
}