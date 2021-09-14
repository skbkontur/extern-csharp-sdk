using System;
    using System.IO;
    using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Uploading
{
    public static class ContentServiceUploadingExtension
    {
        public static Task<Guid> UploadContentByPartsAsync(this IContentService contentService, Guid accountId, Stream stream, TimeSpan? chunkUploadTimeout)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            return stream.Length > contentService.UploadChunkSize
                ? UploadByPartsCoreAsync(contentService, accountId, stream, chunkUploadTimeout)
                : contentService.UploadWholeContentAsync(accountId, stream, chunkUploadTimeout);
        }

        private static async Task<Guid> UploadByPartsCoreAsync(IContentService contentService, Guid accountId, Stream stream, TimeSpan? chunkUploadTimeout)
        {
            var uploadChunkSize = contentService.UploadChunkSize;
            var streamLength = stream.Length;
            var start = 0L;
            Guid contentId;
            var buffer = new byte[uploadChunkSize];
            while (true)
            {
                var read = await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                if (read <= 0)
                    break;

                if (start == 0L)
                {
                    contentId = await contentService.UploadFirstChunkAsync(accountId, buffer, start, streamLength, chunkUploadTimeout).ConfigureAwait(false);
                }
                else
                {
                    await contentService.UploadIntermediateChunkAsync(accountId, contentId, buffer, start, streamLength, chunkUploadTimeout).ConfigureAwait(false);
                }

                start += buffer.Length;
            }

            return contentId;
        }
    }
}