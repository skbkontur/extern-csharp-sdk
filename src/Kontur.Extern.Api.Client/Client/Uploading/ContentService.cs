using System;
using System.IO;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Contents;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Contents;
using Kontur.Extern.Api.Client.Model.Configuration;

namespace Kontur.Extern.Api.Client.Uploading
{
    internal class ContentService : IContentService
    {
        private readonly IContentsClient contents;
        private readonly int downloadChunkSize;

        public ContentService(IContentsClient contents, ContentManagementOptions options)
        {
            UploadChunkSize = options.UploadChunkSize;
            downloadChunkSize = options.DownloadChunkSize;
            this.contents = contents;
        }

        public int UploadChunkSize { get; }

        public Task<Stream> DownloadContentAsync(Guid accountId, Guid contentId, TimeSpan? chunkDownloadTimeout) => 
            contents.DownloadStreamAsync(accountId, contentId, downloadChunkSize, chunkDownloadTimeout);

        public Task<Guid> UploadWholeContentAsync(Guid accountId, byte[] buffer, TimeSpan? timeout) => 
            UploadFirstChunkAsync(accountId, buffer, 0, buffer.Length, timeout);

        public async Task<Guid> UploadWholeContentAsync(Guid accountId, Stream stream, TimeSpan? timeout)
        {
            var response = await contents.StartUploadAsync(accountId, stream, 0, stream.Length - 1, stream.Length, timeout).ConfigureAwait(false);
            return response.Id;
        }
        
        public async Task<Guid> UploadFirstChunkAsync(Guid accountId, byte[] buffer, long from, long? totalLength, TimeSpan? timeout)
        {
            var to = CalcEndForContentRange(from, buffer.Length, totalLength);
            var response = await contents.StartUploadAsync(accountId, buffer, from, to, totalLength, timeout).ConfigureAwait(false);
            return response.Id;
        }

        public async Task<bool> UploadIntermediateChunkAsync(Guid accountId, Guid contentId, byte[] buffer, long from, long? totalLength, TimeSpan? timeout)
        {
            var response = await UploadChunkAsync(accountId, contentId, buffer, from, totalLength, timeout).ConfigureAwait(false);
            return response.IsCompleted;
        }

        private Task<UploadChunkResponse> UploadChunkAsync(Guid accountId, Guid contentId, byte[] buffer, long from, long? totalLength, TimeSpan? timeout)
        {
            var to = CalcEndForContentRange(from, buffer.Length, totalLength);
            return contents.UploadChunkAsync(accountId, contentId, buffer, from, to, totalLength, timeout);
        }

        private static long CalcEndForContentRange(long from, long bufferLength, long? totalLength)
        {
            var minLength = totalLength is null
                ? from + bufferLength
                : Math.Min(from + bufferLength, totalLength.Value);
            return minLength - 1;
        }
    }
}