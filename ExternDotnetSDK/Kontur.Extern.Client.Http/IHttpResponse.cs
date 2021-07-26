namespace Kontur.Extern.Client.Http
{
    public interface IHttpResponse
    {
        HttpStatus Status { get; }
        bool HasPayload { get; }

        byte[] GetBytes();
        string GetString();
        TResponseMessage GetMessage<TResponseMessage>();
        bool TryGetMessage<TResponseMessage>(out TResponseMessage responseMessage);
    }
}