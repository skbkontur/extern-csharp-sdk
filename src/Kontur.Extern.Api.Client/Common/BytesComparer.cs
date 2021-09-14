using System;

namespace Kontur.Extern.Api.Client.Common
{
    public static class BytesComparer
    {
        public static bool IsZero(byte[] bytes)
        {
            var firstBytes = Math.Min(bytes.Length, 8);
            for (var i = 0; i < firstBytes; i++)
            {
                if (bytes[i] != 0)
                    return false;
            }
            
            return firstBytes < 8 || IsZeroByFixedLongUnrolled(bytes);
        }

        /// <summary>
        /// based on solution from
        /// https://stackoverflow.com/questions/33294580/sse-instruction-to-check-if-byte-array-is-zeroes-c-sharp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static unsafe bool IsZeroByFixedLongUnrolled(byte[] data)
        {
            fixed (byte* bytes = data)
            {
                var len = data.Length;
                var remained = len%(sizeof (long)*16);
                var position = (long*) bytes;
                var alignedEnd = (long*) (bytes + len - remained);

                while (position < alignedEnd)
                {
                    if ((*position | *(position + 1) | *(position + 2) | *(position + 3) | *(position + 4) |
                         *(position + 5) | *(position + 6) | *(position + 7) | *(position + 8) |
                         *(position + 9) | *(position + 10) | *(position + 11) | *(position + 12) |
                         *(position + 13) | *(position + 14) | *(position + 15)) != 0)
                    {
                        return false;
                    }

                    position += 16;
                }

                for (var i = 0; i < remained; i++)
                {
                    if (data[len - 1 - i] != 0)
                        return false;
                }

                return true;
            }
        }
    }
}