namespace Kontur.Extern.Client.HttpLevel
{
    public interface IHttpResponse
    {
        byte[] GetBytes();
        string GetString();
        TResponseMessage GetMessage<TResponseMessage>();
    }
}