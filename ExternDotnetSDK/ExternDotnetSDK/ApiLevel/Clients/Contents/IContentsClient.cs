using System;
using System.IO;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Responses.Contents;

namespace Kontur.Extern.Client.ApiLevel.Clients.Contents
{
    public interface IContentsClient
    {
        Task<ContentResponse> StartUploadAsync(Guid accountId, byte[] content, long from, long to, long? contentLength, TimeSpan? timeout = null);
        Task<ContentResponse> StartUploadAsync(Guid accountId, Stream stream, long from, long to, long? contentLength, TimeSpan? timeout = null);
        Task<UploadChunkResponse> UploadChunkAsync(Guid accountId, Guid contentId, byte[] content, long from, long to, long? contentLength, TimeSpan? timeout = null);
        Task<byte[]> DownloadAsBytesAsync(Guid accountId, Guid contentId, TimeSpan? timeout = null);
        Task<(ArraySegment<byte> contentPart, long totalLength)> DownloadAsBytesAsync(Guid accountId, Guid contentId, long from, long to, TimeSpan? timeout = null);
        Task<Stream> DownloadStreamAsync(Guid accountId, Guid contentId, int downloadChunkSize, TimeSpan? timeout = null);
    }
}