using System;
using System.IO;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Contents;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Http.Constants;
using Kontur.Extern.Api.Client.Http.Contents;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Contents
{
    public class ContentsClient : IContentsClient
    {
        private readonly IHttpRequestFactory http;

        public ContentsClient(IHttpRequestFactory http) => this.http = http;

        public Task<ContentResponse> StartUploadAsync(Guid accountId, byte[] content, long from, long to, long? contentLength = null, TimeSpan? timeout = null)
        {
            var request = http.Post($"v1/{accountId}/contents")
                .WithBytes(content)
                .ContentRange(from, to, contentLength)
                .Accept(ContentTypes.Json);
            return SendRequestAsync<ContentResponse>(request, timeout);
        }

        public Task<ContentResponse> StartUploadAsync(Guid accountId, Stream stream, long from, long to, long? contentLength, TimeSpan? timeout = null)
        {
            var request = http.Post($"v1/{accountId}/contents")
                .WithPayload(new StreamPayload(stream))
                .ContentRange(from, to, contentLength)
                .Accept(ContentTypes.Json);
            return SendRequestAsync<ContentResponse>(request, timeout);
        }

        public Task<UploadChunkResponse> UploadChunkAsync(Guid accountId, Guid contentId, byte[] contentChunk, long from, long to, long? contentLength = null, TimeSpan? timeout = null)
        {
            var request = http.Put($"v1/{accountId}/contents/{contentId}")
                .WithBytes(contentChunk)
                .ContentRange(from, to, contentLength)
                .Accept(ContentTypes.Json);
            return SendRequestAsync<UploadChunkResponse>(request, timeout);
        }

        public Task<Stream> DownloadStreamAsync(Guid accountId, Guid contentId, int downloadChunkSize, TimeSpan? timeout = null) => 
            ChunkContentStream.CreateAsync(range => DownloadAsBytesAsync(accountId, contentId, range.from, range.to, timeout), downloadChunkSize);

        public Task<byte[]> DownloadAsBytesAsync(Guid accountId, Guid contentId, TimeSpan? timeout = null) => 
            http.GetBytesAsync($"v1/{accountId}/contents/{contentId}");

        public async Task<(ArraySegment<byte> contentPart, long totalLength)> DownloadAsBytesAsync(Guid accountId, Guid contentId, long @from, long to, TimeSpan? timeout = null)
        {
            var request = http.Get($"v1/{accountId}/contents/{contentId}").Range(from, to);
            var httpResponse = await request.SendAsync(timeout).ConfigureAwait(false);
            var totalLength = httpResponse.ContentRange.Length;
            if (totalLength == null)
                throw Errors.TheResponseDoesNotHaveContentRangeHeader();
            return (await httpResponse.GetBytesSegmentAsync().ConfigureAwait(false), totalLength.Value);
        }

        private static async Task<TResult> SendRequestAsync<TResult>(IHttpRequest httpRequest, TimeSpan? timeout)
        {
            var httpResponse = await httpRequest.SendAsync(timeout).ConfigureAwait(false);
            return await httpResponse.GetMessageAsync<TResult>().ConfigureAwait(false);
        }
    }
}