namespace Kontur.Extern.Client.Http
{
    public interface IPayloadHttpRequest : IHttpRequest
    {
        IHttpRequest WithPayload(IHttpContent content);
    }
}