using System;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Vostok.Commons.Time;

namespace Kontur.Extern.Api.Client.Http.Options
{
    public class RequestTimeouts
    {
        public static readonly TimeSpan MinTimeout = 0.Ticks();

        public RequestTimeouts()
        {
            DefaultReadTimeout = 40.Seconds();
            DefaultWriteTimeout = 40.Seconds();
            DefaultLongOperationTimeout = 3.Minutes();
        }

        public RequestTimeouts(TimeSpan defaultReadTimeout, TimeSpan defaultWriteTimeout, TimeSpan defaultLongOperationTimeout)
        {
            if (defaultReadTimeout < MinTimeout)
                throw Errors.TimeSpanOutOfRange(nameof(defaultReadTimeout), defaultReadTimeout, MinTimeout);
            if (defaultWriteTimeout < MinTimeout)
                throw Errors.TimeSpanOutOfRange(nameof(defaultWriteTimeout), defaultWriteTimeout, MinTimeout);
            if (defaultLongOperationTimeout < MinTimeout)
                throw Errors.TimeSpanOutOfRange(nameof(defaultLongOperationTimeout), defaultLongOperationTimeout, MinTimeout);

            DefaultReadTimeout = defaultReadTimeout;
            DefaultWriteTimeout = defaultWriteTimeout;
            DefaultLongOperationTimeout = defaultLongOperationTimeout;
        }

        public TimeSpan DefaultLongOperationTimeout { get; }
        public TimeSpan DefaultReadTimeout { get; }
        public TimeSpan DefaultWriteTimeout { get; }
    }
}