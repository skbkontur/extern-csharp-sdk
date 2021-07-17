using Kontur.Extern.Client.HttpLevel.Contents;

namespace Kontur.Extern.Client.HttpLevel
{
    public static class PayloadHttpRequestExtension
    {
        public static IHttpRequest WithObject<T>(this IPayloadHttpRequest payloadHttpRequest, T message)
        {
            return message is byte[] bytes 
                ? payloadHttpRequest.WithBytes(bytes) 
                : payloadHttpRequest.WithPayload(ObjectJsonContent.WithMessage(message));
        }

        public static IHttpRequest WithBytes(this IPayloadHttpRequest payloadHttpRequest, byte[] bytes) => 
            payloadHttpRequest.WithPayload(new BytesContent(bytes));

        public static IHttpRequest WithJson(this IPayloadHttpRequest payloadHttpRequest, string json) => 
            payloadHttpRequest.WithPayload(new StringJsonContent(json));
    }
}