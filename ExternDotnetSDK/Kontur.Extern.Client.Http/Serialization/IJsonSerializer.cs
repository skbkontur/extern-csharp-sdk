using System;
using System.IO;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Http.Serialization
{
    public interface IJsonSerializer
    {
        ValueTask<TResult?> DeserializeAsync<TResult>(Stream stream);
        TResult? Deserialize<TResult>(ArraySegment<byte> arraySegment);
        TResult Deserialize<TResult>(string jsonText);

        ValueTask SerializeToJsonStreamAsync<T>(T body, Stream stream);
        ArraySegment<byte> SerializeToJsonBytes<T>(T body);
        string SerializeToIndentedString<T>(T instance);
    }
}