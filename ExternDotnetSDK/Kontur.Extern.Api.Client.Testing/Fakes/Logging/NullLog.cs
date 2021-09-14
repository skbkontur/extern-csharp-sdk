using Vostok.Logging.Abstractions;
using Vostok.Logging.Abstractions.Wrappers;

namespace Kontur.Extern.Api.Client.Testing.Fakes.Logging
{
    public class NullLog : ILog
    {
        public static readonly NullLog Instance = new NullLog();

        private NullLog()
        {
        }

        public void Log(LogEvent @event)
        {
        }

        public bool IsEnabledFor(LogLevel level) => false;

        public ILog ForContext(string context) => new SourceContextWrapper(this, context);
    }
}