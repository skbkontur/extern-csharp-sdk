using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Contents
{
    public interface IContentsService
    {
        Task<Guid> UploadAsync(Guid accountId, byte[] content, string contentType = null, TimeSpan? timeout = null);
        Task<byte[]> DownloadAsync(Guid accountId, Guid contentId, TimeSpan? timeout = null);
    }
}