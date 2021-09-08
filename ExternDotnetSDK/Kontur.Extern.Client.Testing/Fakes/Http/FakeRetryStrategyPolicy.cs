using System;
using System.Collections.Generic;
using Kontur.Extern.Client.Http.Retries;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Retry;

namespace Kontur.Extern.Client.Testing.Fakes.Http
{
    public class FakeRetryStrategyPolicy : IRetryStrategyPolicy
    {
        private Action<int>? onAttemptHandler;
        private List<string>? idempotentHttpMethods;

        public FakeRetryStrategyPolicy() => IdempotentRequests = new FakeIdempotentRequests(this);

        public void UseDefaultIdempotentHttpMethods()
        {
            var defaultIdempotentHttpMethods = HttpMethodBasedIdempotentRequestSpecification.SemanticallyIdempotentMethods.IdempotentHttpMethods;
            idempotentHttpMethods = new List<string>(defaultIdempotentHttpMethods);
        }

        public void NoneIdempotentRequests() => 
            idempotentHttpMethods = new List<string>();

        public int AttemptsCount { get; set; }

        public IIdempotentRequestSpecification IdempotentRequests { get; }

        public IRetryStrategy CreateRetryStrategy() => new FakeImmediateRetryStrategy(this);

        public void ImmediateAttemptsToRepeat(int attempts) => AttemptsCount = attempts;

        public void OnAttempt(Action<int> onAttempt) => onAttemptHandler = onAttempt;

        private class FakeIdempotentRequests : IIdempotentRequestSpecification
        {
            private readonly FakeRetryStrategyPolicy fakePolicy;

            public FakeIdempotentRequests(FakeRetryStrategyPolicy fakePolicy) => 
                this.fakePolicy = fakePolicy;

            public bool IsIdempotent(Request request) => 
                fakePolicy.idempotentHttpMethods is null || fakePolicy.idempotentHttpMethods.Contains(request.Method);
        }

        private class FakeImmediateRetryStrategy : IRetryStrategy
        {
            private readonly FakeRetryStrategyPolicy fakePolicy;

            public FakeImmediateRetryStrategy(FakeRetryStrategyPolicy fakePolicy) => this.fakePolicy = fakePolicy;

            public int AttemptsCount => fakePolicy.AttemptsCount;

            public TimeSpan GetRetryDelay(int attemptsUsed)
            {
                fakePolicy.onAttemptHandler?.Invoke(attemptsUsed);
                return TimeSpan.Zero;
            }
        }
    }
}