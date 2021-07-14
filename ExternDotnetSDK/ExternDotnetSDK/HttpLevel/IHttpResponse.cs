namespace Kontur.Extern.Client.HttpLevel
{
    public interface IHttpResponse
    {
        HttpStatus Status { get; }
        byte[] GetBytes();
        string GetString();
        TResponseMessage GetMessage<TResponseMessage>();
        bool TryGetMessage<TResponseMessage>(out TResponseMessage responseMessage);
    }
}