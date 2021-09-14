using System;
using System.IO;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Common.Streams
{
    public static class StreamReadingExtensions
    {
        public static async ValueTask<byte[]> ToArrayAsync(this Stream stream)
        {
            if (stream is MemoryStream memoryStream)
                return memoryStream.ToArray();
                
            var count = stream.Length - stream.Position;
            if (count == 0)
                return Array.Empty<byte>();
            
            var copy = new byte[count];
            await stream.ReadAsync(copy, 0, copy.Length).ConfigureAwait(false);
            return copy;
        }
    }
}