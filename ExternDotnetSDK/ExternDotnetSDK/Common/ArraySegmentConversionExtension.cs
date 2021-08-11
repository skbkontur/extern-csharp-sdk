using System;
using System.IO;

namespace Kontur.Extern.Client.Common
{
    internal static class ArraySegmentConversionExtension
    {
        public static MemoryStream ToMemoryStreamWithPublicBuffer(this ArraySegment<byte> arraySegment) =>
            arraySegment.Array == null 
                ? new MemoryStream(0) 
                : new MemoryStream(arraySegment.Array, arraySegment.Offset, arraySegment.Count, false, true);
    }
}