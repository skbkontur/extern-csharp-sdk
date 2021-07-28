using System.IO;

namespace Kontur.Extern.Client.Http.Serialization
{
    public interface IJsonSerializer
    {
        void SerializeToJsonStream<T>(T body, Stream stream);
        TResult DeserializeFromJson<TResult>(Stream stream);
    }
}