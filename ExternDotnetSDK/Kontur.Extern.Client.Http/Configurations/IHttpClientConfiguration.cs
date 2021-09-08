using Kontur.Extern.Client.Http.Retries;
using Vostok.Clusterclient.Core;

namespace Kontur.Extern.Client.Http.Configurations
{
    public interface IHttpClientConfiguration
    {
        IIdempotentRequestSpecification? IdempotentRequests { get; }
        IRetryStrategyPolicy? RetryStrategy { get; }
        
        void Apply(IClusterClientConfiguration config);
    }
}