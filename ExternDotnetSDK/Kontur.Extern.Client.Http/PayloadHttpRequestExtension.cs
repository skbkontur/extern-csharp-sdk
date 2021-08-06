using Kontur.Extern.Client.Http.Contents;

namespace Kontur.Extern.Client.Http
{
    public static class PayloadHttpRequestExtension
    {
        public static IPayloadHttpRequest WithObject<T>(this IPayloadHttpRequest payloadHttpRequest, T message)
        {
            return message is byte[] bytes 
                ? payloadHttpRequest.WithBytes(bytes) 
                : payloadHttpRequest.WithPayload(ObjectJsonContent.WithMessage(message));
        }

        public static IPayloadHttpRequest WithBytes(this IPayloadHttpRequest payloadHttpRequest, byte[] bytes) => 
            payloadHttpRequest.WithPayload(new BytesContent(bytes));

        public static IPayloadHttpRequest WithJson(this IPayloadHttpRequest payloadHttpRequest, string json) => 
            payloadHttpRequest.WithPayload(new StringJsonContent(json));
    }
}