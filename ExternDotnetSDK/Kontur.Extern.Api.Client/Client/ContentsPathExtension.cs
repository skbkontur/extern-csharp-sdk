using System;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.Uploading;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class ContentsPathExtension
    {
        public static Task<Stream> DownloadAsStreamAsync(this in ContentsPath path, Guid contentId, TimeSpan? timeout = null)
        {
            var contentService = path.Services.ContentService;
            return contentService.DownloadContentAsync(path.AccountId, contentId, timeout);
        }
        
        public static Task<Guid> UploadAsync(this in ContentsPath path, Stream stream, TimeSpan? chunkUploadTimeout = null)
        {
            var contentService = path.Services.ContentService;
            return contentService.UploadContentByPartsAsync(path.AccountId, stream, chunkUploadTimeout);
        }
    }
}