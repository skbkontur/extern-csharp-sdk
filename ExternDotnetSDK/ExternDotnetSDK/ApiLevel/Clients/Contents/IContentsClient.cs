using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Contents;

namespace Kontur.Extern.Client.ApiLevel.Clients.Contents
{
    public interface IContentsClient
    {
        Task<ContentResponse> UploadAsync(Guid accountId, byte[] content, string contentType = null, TimeSpan? timeout = null);
        Task<ContentResponse> StartUploadAsync(Guid accountId, byte[] contentChunk, long @from, long to, long contentLength, string contentType = null, TimeSpan? timeout = null);
        Task<UploadChunkResponse> UploadChunkAsync(Guid accountId, Guid contentId, byte[] contentChunk, int from, int to, TimeSpan? timeout = null);
        Task<byte[]> DownloadAsync(Guid accountId, Guid contentId, TimeSpan? timeout = null);
        Task<byte[]> DownloadAsync(Guid accountId, Guid contentId, int from, int to, TimeSpan? timeout = null);
    }
}