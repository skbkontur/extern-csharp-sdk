using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Client.Testing.Lifetimes;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool
{
    internal class DriveCertificatesReader
    {
        private readonly ILifetime lifetime;
        private readonly HttpClient httpClient;

        public DriveCertificatesReader(ILifetime lifetime)
        {
            this.lifetime = lifetime;
            httpClient = lifetime.Add(new HttpClient());
        }

        public async Task<byte[]> GetPublicPartOfCertificate(string driveCertificateUrl)
        {
            var positionOfFileName = driveCertificateUrl.LastIndexOf("/", StringComparison.Ordinal) + 1;
            var fileName = driveCertificateUrl.Substring(positionOfFileName);
            var directoryName = Path.GetFileNameWithoutExtension(fileName);
            lifetime.Add(() => DeleteDirectoryIfExists(directoryName));
                
            if (!File.Exists(fileName))
            {
                await DownloadToFile(driveCertificateUrl, fileName);
            }

            if (!Directory.Exists(directoryName) || Directory.GetFiles(directoryName).Length == 0)
            {
                ZipFile.ExtractToDirectory(fileName, directoryName);
            }
            
            var publicPartOfCert = Path.Combine(directoryName, "cert.cer");
            return await File.ReadAllBytesAsync(publicPartOfCert);

            async Task DownloadToFile(string url, string path)
            {
                await using var stream = await httpClient.GetStreamAsync(new Uri(url, UriKind.Absolute));
                await using var fileStream = File.OpenWrite(path);
                await stream.CopyToAsync(fileStream);
            }
        }

        private static void DeleteDirectoryIfExists(string? directoryName)
        {
            try
            {
                Directory.Delete(directoryName);
            }
            catch (Exception)
            {
                /* IGNORE */
            }
        }
    }
}