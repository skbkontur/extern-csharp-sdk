#nullable enable
using System.IO;

namespace Kontur.Extern.Client.HttpLevel.Serialization
{
    public interface IJsonSerializer
    {
        void SerializeToJsonStream<T>(T body, Stream stream);
        TResult DeserializeFromJson<TResult>(Stream stream);
    }
}