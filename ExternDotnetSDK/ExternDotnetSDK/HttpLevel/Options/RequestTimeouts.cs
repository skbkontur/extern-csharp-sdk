using System;
using Kontur.Extern.Client.Exceptions;
using Vostok.Commons.Time;

namespace Kontur.Extern.Client.HttpLevel.Options
{
    public class RequestTimeouts
    {
        private static readonly TimeSpan MinTimeout = 5.Seconds();
        private static readonly TimeSpan MaxTimeout = 3.Minutes();
        
        public RequestTimeouts()
        {
            DefaultReadTimeout = 40.Seconds();
            DefaultWriteTimeout = 40.Seconds();
        }

        public RequestTimeouts(TimeSpan defaultReadTimeout, TimeSpan defaultWriteTimeout)
        {
            if (defaultReadTimeout < MinTimeout || defaultReadTimeout > MaxTimeout)
                throw Errors.TimeSpanOutOfRange(nameof(defaultReadTimeout), defaultReadTimeout, MinTimeout, MaxTimeout);
            if (defaultWriteTimeout < MinTimeout || defaultWriteTimeout > MaxTimeout)
                throw Errors.TimeSpanOutOfRange(nameof(defaultWriteTimeout), defaultWriteTimeout, MinTimeout, MaxTimeout);
            
            DefaultReadTimeout = defaultReadTimeout;
            DefaultWriteTimeout = defaultWriteTimeout;
        }

        public TimeSpan DefaultReadTimeout { get; }
        public TimeSpan DefaultWriteTimeout { get; }
    }
}