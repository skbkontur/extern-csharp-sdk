#nullable enable
using System.IO;

namespace Kontur.Extern.Client.Clients.Common.Requests
{
    public interface IRequestBodySerializer
    {
        string SerializeToJson<T>(T body);
        void SerializeToJsonStream<T>(T body, Stream stream);

        TResult DeserializeFromJson<TResult>(Stream stream);
    }
}