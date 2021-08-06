using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Clients.Contents;

namespace Kontur.Extern.Client.Contents
{
    public class ContentsService : IContentsService
    {
        private const int ChunkSize = 1*1024*1024;

        private readonly IContentsClient contentsClient;

        public ContentsService(IContentsClient contentsClient) => this.contentsClient = contentsClient;

        public async Task<Guid> UploadAsync(Guid accountId, byte[] content, string contentType = null, TimeSpan? timeout = null)
        {
            if (content.Length < ChunkSize)
            {
                var contentResponse = await contentsClient.UploadAsync(accountId, content, contentType, timeout).ConfigureAwait(false);
                return contentResponse.Id;
            }

            return await UploadByChunks(accountId, content, contentType, timeout).ConfigureAwait(false);
        }

        public Task<byte[]> DownloadAsync(Guid accountId, Guid contentId, TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }

        private Task<Guid> UploadByChunks(Guid accountId, byte[] content, string contentType, TimeSpan? timeout)
        {
            throw new NotImplementedException();
        }
    }
}