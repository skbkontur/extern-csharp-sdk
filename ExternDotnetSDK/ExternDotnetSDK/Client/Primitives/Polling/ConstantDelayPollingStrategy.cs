using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Primitives.Polling
{
    internal class ConstantDelayPollingStrategy : IPollingStrategy
    {
        private readonly IPolling delayPolling;

        public ConstantDelayPollingStrategy(TimeSpan delay) => delayPolling = new DelayPolling(delay);

        public IPolling Start() => delayPolling;

        private class DelayPolling : IPolling
        {
            private readonly TimeSpan delay;

            public DelayPolling(TimeSpan delay) => this.delay = delay;

            public Task WaitForNextAttempt() => Task.Delay(delay);
        }
    }
}