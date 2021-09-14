using Kontur.Extern.Api.Client.Http.Retries;
using Vostok.Clusterclient.Core;

namespace Kontur.Extern.Api.Client.Http.Configurations
{
    public interface IHttpClientConfiguration
    {
        IIdempotentRequestSpecification? IdempotentRequests { get; }
        IRetryStrategyPolicy? RetryStrategy { get; }
        
        void Apply(IClusterClientConfiguration config);
    }
}