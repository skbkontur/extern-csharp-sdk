using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Testing.Lifetimes;

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.Drive
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

        public async Task<byte[]?> GetPublicPartOfCertificate(string driveCertificateUrl)
        {
            var positionOfFileName = driveCertificateUrl.LastIndexOf("/", StringComparison.Ordinal) + 1;
            
            var fileName = driveCertificateUrl.Substring(positionOfFileName);
            lifetime.Add(() => DeleteFileIfExists(fileName));
            
            var directoryName = Path.GetFileNameWithoutExtension(fileName);
            lifetime.Add(() => DeleteDirectoryIfExists(directoryName));

            if (!File.Exists(fileName))
            {
                var downloaded = await DownloadToFile(driveCertificateUrl, fileName);
                if (!downloaded)
                    return null;
            }

            if (!Directory.Exists(directoryName) || Directory.GetFiles(directoryName).Length == 0)
            {
                ZipFile.ExtractToDirectory(fileName, directoryName);
            }
            
            var publicPartOfCert = Path.Combine(directoryName, "cert.cer");
            return await File.ReadAllBytesAsync(publicPartOfCert);

            async Task<bool> DownloadToFile(string url, string path)
            {
                var responseMessage = await httpClient.GetAsync(new Uri(url, UriKind.Absolute));
                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                    return false;
                responseMessage.EnsureSuccessStatusCode();
                
                var content = responseMessage.Content;
                await using var stream = await content.ReadAsStreamAsync().ConfigureAwait(false);
                await using var fileStream = File.OpenWrite(path);
                await stream.CopyToAsync(fileStream);

                return true;
            }
        }

        private static void DeleteDirectoryIfExists(string directoryName)
        {
            try
            {
                Directory.Delete(directoryName, true);
            }
            catch (Exception)
            {
                /* IGNORE */
            }
        }

        private static void DeleteFileIfExists(string fileName)
        {
            try
            {
                File.Delete(fileName);
            }
            catch (Exception)
            {
                /* IGNORE */
            }
        }
    }
}