namespace Kontur.Extern.Api.Client.Http.Retries
{
    public interface IRetryStrategyPolicy
    {
        Vostok.Clusterclient.Core.Retry.IRetryStrategy CreateRetryStrategy();
    }
}