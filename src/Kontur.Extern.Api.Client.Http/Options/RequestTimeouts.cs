using System;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Vostok.Commons.Time;

namespace Kontur.Extern.Api.Client.Http.Options
{
    public class RequestTimeouts
    {
        public static readonly TimeSpan MinTimeout = 5.Seconds();
        public static readonly TimeSpan MaxTimeout = 3.Minutes();
        
        public RequestTimeouts()
        {
            DefaultReadTimeout = 40.Seconds();
            DefaultWriteTimeout = 40.Seconds();
            DefaultLongOperationTimeout = MaxTimeout;
        }

        public RequestTimeouts(TimeSpan defaultReadTimeout, TimeSpan defaultWriteTimeout, TimeSpan defaultLongOperationTimeout)
        {
            if (defaultReadTimeout < MinTimeout || defaultReadTimeout > MaxTimeout)
                throw Errors.TimeSpanOutOfRange(nameof(defaultReadTimeout), defaultReadTimeout, MinTimeout, MaxTimeout);
            if (defaultWriteTimeout < MinTimeout || defaultWriteTimeout > MaxTimeout)
                throw Errors.TimeSpanOutOfRange(nameof(defaultWriteTimeout), defaultWriteTimeout, MinTimeout, MaxTimeout);
            if (defaultLongOperationTimeout < MinTimeout || defaultLongOperationTimeout > MaxTimeout)
                throw Errors.TimeSpanOutOfRange(nameof(defaultLongOperationTimeout), defaultLongOperationTimeout, MinTimeout, MaxTimeout);
            
            DefaultReadTimeout = defaultReadTimeout;
            DefaultWriteTimeout = defaultWriteTimeout;
            DefaultLongOperationTimeout = defaultLongOperationTimeout;
        }

        public TimeSpan DefaultLongOperationTimeout { get; }
        public TimeSpan DefaultReadTimeout { get; }
        public TimeSpan DefaultWriteTimeout { get; }

        public void ValidateCustomTimeout(TimeSpan timeout)
        {
            if (timeout < MinTimeout || timeout > MaxTimeout)
                throw Errors.TimeSpanOutOfRange(nameof(timeout), timeout, MinTimeout, MaxTimeout);
        }
    }
}