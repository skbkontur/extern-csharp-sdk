namespace Kontur.Extern.Client.HttpLevel
{
    public interface IPayloadHttpRequest : IHttpRequest
    {
        IHttpRequest WithPayload<TRequestMessage>(TRequestMessage message);
        IHttpRequest WithJsonPayload(string json);
    }
}