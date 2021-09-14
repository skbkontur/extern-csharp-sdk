using System;
using System.IO;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Http.Serialization
{
    public interface IJsonSerializer
    {
        ValueTask<DeserializationResult<TResult>> TryDeserializeAsync<TResult>(Stream stream);
        DeserializationResult<TResult> TryDeserialize<TResult>(in ArraySegment<byte> arraySegment);
        DeserializationResult<TResult> TryDeserialize<TResult>(string jsonText);

        ValueTask SerializeToJsonStreamAsync<T>(T body, Stream stream);
        ArraySegment<byte> SerializeToJsonBytes<T>(T body);
        string SerializeToIndentedString<T>(T instance);
    }
}