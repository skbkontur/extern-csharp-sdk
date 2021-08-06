namespace Kontur.Extern.Client.Http
{
    public interface IPayloadHttpRequest : IHttpRequest
    {
        IPayloadSpecifiedRequest WithPayload(IHttpContent content);
    }
}