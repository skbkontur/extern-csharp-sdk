using System;
using System.IO;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Http.Serialization
{
    public static class JsonSerializerExtension
    {
        public static async ValueTask<TResult> DeserializeAsync<TResult>(this IJsonSerializer serializer, Stream stream)
        {
            var result = await serializer.TryDeserializeAsync<TResult>(stream).ConfigureAwait(false);
            return result.EnsureSuccessfulNotNullResult();
        }

        public static TResult Deserialize<TResult>(this IJsonSerializer serializer, in ArraySegment<byte> arraySegment)
        {
            var result = serializer.TryDeserialize<TResult>(arraySegment);
            return result.EnsureSuccessfulNotNullResult();
        }

        public static TResult Deserialize<TResult>(this IJsonSerializer serializer, string jsonText)
        {
            var result = serializer.TryDeserialize<TResult>(jsonText);
            return result.EnsureSuccessfulNotNullResult();
        }
    }
}