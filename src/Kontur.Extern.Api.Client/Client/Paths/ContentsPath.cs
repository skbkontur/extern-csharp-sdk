using System;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Uploading;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct ContentsPath
    {
        public ContentsPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public Task<Stream> DownloadAsStreamAsync(Guid contentId, TimeSpan? timeout = null)
        {
            var contentService = Services.ContentService;
            return contentService.DownloadContentAsync(AccountId, contentId, timeout);
        }

        public Task<Guid> UploadAsync(Stream stream, TimeSpan? chunkUploadTimeout = null)
        {
            var contentService = Services.ContentService;
            return contentService.UploadContentByPartsAsync(AccountId, stream, chunkUploadTimeout);
        }
    }
}