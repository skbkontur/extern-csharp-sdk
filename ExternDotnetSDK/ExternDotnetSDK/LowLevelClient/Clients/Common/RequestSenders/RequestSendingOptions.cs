using System;

namespace Kontur.Extern.Client.Clients.Common.RequestSenders
{
    internal class RequestSendingOptions
    {
        // todo: specify default timeouts
        public TimeSpan? DefaultReadTimeout { get; } = TimeSpan.FromSeconds(5);
        public TimeSpan? DefaultWriteTimeout { get; } = TimeSpan.FromSeconds(5);
    }
}