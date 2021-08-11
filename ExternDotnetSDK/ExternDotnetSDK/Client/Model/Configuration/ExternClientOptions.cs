namespace Kontur.Extern.Client.Model.Configuration
{
    public class ExternClientOptions
    {
        private const int DefaultUploadChunkSize = 1*1024*1024; // NOTE: 1Mb
        private const int DefaultDownloadChunkSize = 80*1024; // NOTE: 80Kb
        
        public ExternClientOptions()
        {
            UploadChunkSize = DefaultUploadChunkSize;
            DownloadChunkSize = DefaultDownloadChunkSize;
        }

        public int UploadChunkSize { get; }
        public int DownloadChunkSize { get; }
    }
}