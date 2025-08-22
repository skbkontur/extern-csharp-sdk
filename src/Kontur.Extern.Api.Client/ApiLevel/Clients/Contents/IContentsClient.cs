using System;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Contents;
using Kontur.Extern.Api.Client.Attributes;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Contents
{
    [PublicAPI]
    [ClientDocumentationSection]
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