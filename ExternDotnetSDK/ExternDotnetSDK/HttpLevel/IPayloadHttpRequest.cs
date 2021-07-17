namespace Kontur.Extern.Client.HttpLevel
{
    public interface IPayloadHttpRequest : IHttpRequest
    {
        IHttpRequest WithPayload(IHttpContent content);
    }
}