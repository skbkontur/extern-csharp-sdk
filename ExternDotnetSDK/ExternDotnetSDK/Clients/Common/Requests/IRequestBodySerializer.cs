namespace Kontur.Extern.Client.Clients.Common.Requests
{
    public interface IRequestBodySerializer
    {
        string SerializeToJson<T>(T body);
    }
}