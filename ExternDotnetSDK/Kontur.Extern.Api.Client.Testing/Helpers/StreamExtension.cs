using System.IO;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Testing.Helpers
{
    public static class StreamExtension
    {
        public static byte[] ReadAllBytes(this Stream stream)
        {
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
        
        public static async Task<byte[]> ReadAllBytesAsync(this Stream stream)
        {
            await using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}