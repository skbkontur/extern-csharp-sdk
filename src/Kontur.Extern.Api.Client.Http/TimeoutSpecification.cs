using System;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters;
using Kontur.Extern.Api.Client.Http.Options;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http
{
    public readonly struct TimeoutSpecification
    {
        public static readonly TimeoutSpecification LongOperationTimeout = new(true, null);
        
        public static readonly TimeoutSpecification RegularOperationTimeout = default;

        public static TimeoutSpecification SpecificTimeout(TimeSpan timeout) => new(false, timeout);
        
        public static TimeoutSpecification SpecificOrLongOperationTimeout(TimeSpan? timeout) => 
            timeout.HasValue 
                ? SpecificTimeout(timeout.Value) 
                : LongOperationTimeout;
        
        public static TimeoutSpecification SpecificOrRegularOperationTimeout(TimeSpan? timeout) => 
            timeout.HasValue 
                ? SpecificTimeout(timeout.Value) 
                : RegularOperationTimeout;
        
        private readonly bool isLongOperation;
        private readonly TimeSpan? specificTimeout;

        public TimeoutSpecification(bool isLongOperation, TimeSpan? specificTimeout)
        {
            this.isLongOperation = isLongOperation;
            this.specificTimeout = specificTimeout;
        }
        
        public TimeSpan GetTimeout(Request request, RequestTimeouts timeouts)
        {
            if (specificTimeout.HasValue)
            {
                timeouts.ValidateCustomTimeout(specificTimeout.Value);
                return specificTimeout.Value;
            }

            if (isLongOperation)
                return timeouts.DefaultLongOperationTimeout;

            return request.IsWriteRequest() 
                ? timeouts.DefaultWriteTimeout 
                : timeouts.DefaultReadTimeout;
        }

        public static implicit operator TimeoutSpecification(TimeSpan? timeout) => SpecificOrRegularOperationTimeout(timeout);
    }
}