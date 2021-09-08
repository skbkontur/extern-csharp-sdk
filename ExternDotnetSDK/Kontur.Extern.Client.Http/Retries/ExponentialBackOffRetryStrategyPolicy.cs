using System;
using Kontur.Extern.Client.Http.Exceptions;
using Vostok.Clusterclient.Core.Retry;

namespace Kontur.Extern.Client.Http.Retries
{
    public class ExponentialBackOffRetryStrategyPolicy : IRetryStrategyPolicy
    {
        private readonly int attemptsCount;
        private readonly TimeSpan initialRetryDelay;
        private readonly TimeSpan maxRetryDelay;

        public ExponentialBackOffRetryStrategyPolicy(
            int attemptsCount = 10,
            int initialRetryDelayMilliseconds = 10*1000,
            int maxRetryDelayMilliseconds = 10*1000)
        {
            if (attemptsCount < 2)
                throw Errors.ValueShouldBeGreaterThan(nameof(attemptsCount), attemptsCount, 2);
            if (initialRetryDelayMilliseconds < 0)
                throw Errors.ValueShouldBeGreaterThan(nameof(initialRetryDelayMilliseconds), initialRetryDelayMilliseconds, 0);
            if (maxRetryDelayMilliseconds < 0)
                throw Errors.ValueShouldBeGreaterThan(nameof(maxRetryDelayMilliseconds), maxRetryDelayMilliseconds, 0);
            if (maxRetryDelayMilliseconds < initialRetryDelayMilliseconds)
                throw Errors.InvalidRange(nameof(initialRetryDelayMilliseconds), nameof(maxRetryDelayMilliseconds), initialRetryDelayMilliseconds, maxRetryDelayMilliseconds);

            this.attemptsCount = attemptsCount;
            initialRetryDelay = TimeSpan.FromMilliseconds(initialRetryDelayMilliseconds);
            maxRetryDelay = TimeSpan.FromMilliseconds(maxRetryDelayMilliseconds);
        }

        public IRetryStrategy CreateRetryStrategy() => 
            new ExponentialBackoffRetryStrategy(attemptsCount, initialRetryDelay, maxRetryDelay);
    }
}