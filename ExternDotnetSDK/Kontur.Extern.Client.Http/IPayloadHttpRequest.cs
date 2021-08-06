namespace Kontur.Extern.Client.Http
{
    public interface IPayloadHttpRequest : IHttpRequest
    {
        IPayloadHttpRequest WithPayload(IHttpContent content);
        IHttpRequest ContentRange(long from, long to);
        IHttpRequest ContentRange(long from, long to, long length);
    }
}