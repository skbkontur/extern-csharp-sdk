using System;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Modules;
using Vostok.Clusterclient.Core.Retry;

namespace Kontur.Extern.Client.Http.ClusterClientAdapters
{
    internal class RetryStrategyExAdapter : IRetryStrategyEx
    {
        private readonly IRetryStrategy retryStrategy;

        public RetryStrategyExAdapter(IRetryStrategy retryStrategy)
        {
            this.retryStrategy = retryStrategy ?? throw new ArgumentNullException(nameof(retryStrategy));
        }

        public TimeSpan? GetRetryDelay(IRequestContext context, ClusterResult lastResult, int attemptsUsed)
        {
            if (attemptsUsed >= retryStrategy.AttemptsCount)
                return null;

            return retryStrategy.GetRetryDelay(attemptsUsed);
        }
    }
}