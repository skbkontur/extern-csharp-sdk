using System;

namespace Kontur.Extern.Client.Concept
{
    public class Options
    {
        public TimeSpan DefaultReadTimeout { get; }
        public TimeSpan DefaultWriteTimeout { get; }
    }
}