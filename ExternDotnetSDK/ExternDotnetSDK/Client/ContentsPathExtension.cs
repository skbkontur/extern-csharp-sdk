using System;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.Paths;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class ContentsPathExtension
    {
        public static Task<Stream> DownloadAsStreamAsync(this in ContentsPath path, Guid contentId, TimeSpan? timeout = null)
        {
            var contentService = path.Services.ContentService;
            return contentService.DownloadContentAsync(path.AccountId, contentId, timeout);
        }
    }
}