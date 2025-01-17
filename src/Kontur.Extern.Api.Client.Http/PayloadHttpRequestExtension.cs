using Kontur.Extern.Api.Client.Http.Contents;

namespace Kontur.Extern.Api.Client.Http
{
    public static class PayloadHttpRequestExtension
    {
        public static IPayloadSpecifiedRequest WithObject<T>(this IPayloadHttpRequest payloadHttpRequest, T message)
        {
            return message is byte[] bytes 
                ? payloadHttpRequest.WithBytes(bytes) 
                : payloadHttpRequest.WithPayload(ObjectJsonContent.WithMessage(message));
        }

        public static IPayloadSpecifiedRequest WithBytes(this IPayloadHttpRequest payloadHttpRequest, byte[] bytes, int offset, int length) => 
            payloadHttpRequest.WithPayload(new BytesContent(bytes, offset, length));

        public static IPayloadSpecifiedRequest WithBytes(this IPayloadHttpRequest payloadHttpRequest, byte[] bytes) => 
            payloadHttpRequest.WithBytes(bytes, 0, bytes.Length);

        public static IPayloadSpecifiedRequest WithJson(this IPayloadHttpRequest payloadHttpRequest, string json) => 
            payloadHttpRequest.WithPayload(new StringJsonContent(json));
    }
}