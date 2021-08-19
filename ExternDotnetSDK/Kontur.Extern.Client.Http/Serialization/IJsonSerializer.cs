using System.IO;
using System.Text;

namespace Kontur.Extern.Client.Http.Serialization
{
    public interface IJsonSerializer
    {
        void SerializeToJsonStream<T>(T body, Stream stream);
        TResult DeserializeFromJson<TResult>(Stream stream);

        void SerializeToIndentedString<T>(T instance, StringBuilder stringBuilder);
    }
}