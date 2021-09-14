namespace Kontur.Extern.Api.Client.Primitives.Polling
{
    public interface IPollingStrategy
    {
        IPolling Start();
    }
}