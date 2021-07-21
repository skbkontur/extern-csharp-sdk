using System;
using Kontur.Extern.Client.Exceptions;
using Vostok.Commons.Time;

namespace Kontur.Extern.Client.HttpLevel.Options
{
    public class RequestSendingOptions
    {
        private static readonly TimeSpan MinTimeout = 1.Seconds();
        private static readonly TimeSpan MaxTimeout = 1.Hours();
        
        public RequestSendingOptions()
        {
            DefaultReadTimeout = TimeSpan.FromSeconds(5);
            DefaultWriteTimeout = TimeSpan.FromSeconds(5);
        }

        public RequestSendingOptions(TimeSpan defaultReadTimeout, TimeSpan defaultWriteTimeout)
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