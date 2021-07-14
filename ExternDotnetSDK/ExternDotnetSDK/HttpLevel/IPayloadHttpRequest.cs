namespace Kontur.Extern.Client.HttpLevel
{
    public interface IPayloadHttpRequest : IHttpRequest
    {
        IHttpRequest WithPayload<TRequestMessage>(TRequestMessage message);
        IHttpRequest WithFormUrlEncoded(string content);
        IHttpRequest WithJsonPayload(string json);
    }
}