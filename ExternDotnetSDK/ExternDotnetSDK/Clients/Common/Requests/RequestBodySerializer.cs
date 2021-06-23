using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Clients.Common.Requests
{
    public class RequestBodySerializer : IRequestBodySerializer
    {
        private readonly JsonSerializer jsonSerializer;

        public RequestBodySerializer()
        {
            jsonSerializer = new JsonSerializer();
        }

        public string SerializeToJson<T>(T body)
        {
            var stringBuilder = new StringBuilder();
            jsonSerializer.Serialize(new StringWriter(stringBuilder), body);
            return stringBuilder.ToString();
        }
    }
}