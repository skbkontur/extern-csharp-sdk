using System;
using System.Runtime.CompilerServices;

namespace Kontur.Extern.Api.Client.Http
{
    public static class StringToUriConversionExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Uri ToUrl(this string url) => new(url, UriKind.RelativeOrAbsolute);
    }
}