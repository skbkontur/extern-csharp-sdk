namespace Kontur.Extern.Api.Client.Http
{
    public interface IPayloadHttpRequest : IHttpRequest
    {
        IPayloadSpecifiedRequest WithPayload(IHttpContent content);
    }
}