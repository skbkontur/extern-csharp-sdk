using System;
using System.IO;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Uploading
{
    public interface IContentService
    {
        int UploadChunkSize { get; }

        Task<Stream> DownloadContentAsync(Guid accountId, Guid contentId, TimeSpan? chunkDownloadTimeout);

        Task<Guid> UploadWholeContentAsync(Guid accountId, byte[] buffer, TimeSpan? timeout);
        Task<Guid> UploadWholeContentAsync(Guid accountId, Stream stream, TimeSpan? timeout);
        Task<Guid> UploadFirstChunkAsync(Guid accountId, byte[] buffer, long from, long? totalLength, TimeSpan? timeout);
        Task<bool> UploadIntermediateChunkAsync(Guid accountId, Guid contentId, byte[] buffer, long from, long? totalLength, TimeSpan? timeout);
    }
}