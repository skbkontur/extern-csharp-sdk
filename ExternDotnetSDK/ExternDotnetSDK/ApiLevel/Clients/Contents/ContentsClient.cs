using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Contents;
using Kontur.Extern.Client.Http;
using Kontur.Extern.Client.Http.Constants;

namespace Kontur.Extern.Client.ApiLevel.Clients.Contents
{
    public class ContentsClient : IContentsClient
    {
        private readonly IHttpRequestsFactory http;

        public ContentsClient(IHttpRequestsFactory http) => this.http = http;

        public Task<ContentResponse> UploadAsync(Guid accountId, byte[] content, string contentType = null, TimeSpan? timeout = null) => 
            StartUploadAsync(accountId, content, 0, content.Length - 1, content.Length, contentType, timeout);

        public Task<ContentResponse> StartUploadAsync(Guid accountId, byte[] contentChunk, long from, long to, long contentLength, string contentType = null, TimeSpan? timeout = null)
        {
            var request = http.Post($"v1/{accountId}/contents")
                .WithBytes(contentChunk)
                .ContentRange(from, to, contentLength)
                .Accept(ContentTypes.Json);
            return SendRequestAsync<ContentResponse>(request, timeout);
        }

        public Task<UploadChunkResponse> UploadChunkAsync(Guid accountId, Guid contentId, byte[] contentChunk, int from, int to, TimeSpan? timeout = null)
        {
            var request = http.Put($"v1/{accountId}/contents/{contentId}")
                .WithBytes(contentChunk)
                .ContentRange(from, to)
                .Accept(ContentTypes.Json);
            return SendRequestAsync<UploadChunkResponse>(request, timeout);
        }

        public Task<byte[]> DownloadAsync(Guid accountId, Guid contentId, TimeSpan? timeout = null) => 
            http.GetBytesAsync($"v1/{accountId}/contents/{contentId}");

        public async Task<byte[]> DownloadAsync(Guid accountId, Guid contentId, int from, int to, TimeSpan? timeout = null)
        {
            var request = http.Get($"v1/{accountId}/contents/{contentId}").Range(from, to);
            var httpResponse = await request.SendAsync(timeout).ConfigureAwait(false);
            return httpResponse.GetBytes();
        }

        private static async Task<TResult> SendRequestAsync<TResult>(IHttpRequest httpRequest, TimeSpan? timeout)
        {
            var httpResponse = await httpRequest.SendAsync(timeout).ConfigureAwait(false);
            return httpResponse.GetMessage<TResult>();
        }
    }
}