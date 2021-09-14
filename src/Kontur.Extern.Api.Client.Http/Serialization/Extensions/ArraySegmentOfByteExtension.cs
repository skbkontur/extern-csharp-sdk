using System;
using System.IO;

namespace Kontur.Extern.Api.Client.Http.Serialization.Extensions
{
    internal static class ArraySegmentOfByteExtension
    {
        public static MemoryStream AsMemoryStream(this ArraySegment<byte> arraySegment)
        {
            var buffer = arraySegment.Array ?? throw new ArgumentNullException(nameof(arraySegment.Array));
            return new(buffer, arraySegment.Offset, arraySegment.Count, false, true);
        }
    }
}