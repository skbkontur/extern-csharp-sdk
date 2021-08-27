using System;
using System.IO;

namespace Kontur.Extern.Client.Http.Serialization
{
    public interface IJsonSerializer
    {
        TResult DeserializeFromJson<TResult>(Stream stream);
        TResult DeserializeFromJson<TResult>(ArraySegment<byte> arraySegment);
        TResult DeserializeFromJson<TResult>(string jsonText);

        void SerializeToJsonStream<T>(T body, Stream stream);
        ArraySegment<byte> SerializeToJsonBytes<T>(T body);
        string SerializeToIndentedString<T>(T instance);
    }
}