using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model.Configuration
{
    public class ContentManagementOptions
    {
        public static readonly ContentManagementOptions Default = new();
        
        private const int DefaultUploadChunkSize = 1*1024*1024; // NOTE: 1Mb
        private const int DefaultDownloadChunkSize = 1*1024*1024; // NOTE: 1Mb

        public ContentManagementOptions(int uploadChunkSize = DefaultUploadChunkSize, int downloadChunkSize = DefaultDownloadChunkSize)
        {
            if (uploadChunkSize <= 0)
                throw Errors.ValueShouldBeGreaterThanZero(nameof(uploadChunkSize), uploadChunkSize);
            if (downloadChunkSize <= 0)
                throw Errors.ValueShouldBeGreaterThanZero(nameof(downloadChunkSize), downloadChunkSize);
            
            UploadChunkSize = uploadChunkSize;
            DownloadChunkSize = downloadChunkSize;
        }

        public int UploadChunkSize { get; }
        public int DownloadChunkSize { get; }
    }
}