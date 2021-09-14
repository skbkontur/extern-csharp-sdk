using System;
using System.IO;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Abstractions.Wrappers;
using Vostok.Logging.Formatting;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.Testing.Fakes.Logging
{
    public class TestLog : ILog
    {
        private Action<string> writeLine;

        public TestLog(ITestOutputHelper output) => writeLine = output.WriteLine;

        public TestLog(IMessageSink messageSink) => writeLine = e => messageSink.OnMessage(new DiagnosticMessage(e));

        public void Log(LogEvent @event)
        {
            using var writer = new StringWriter(); 
            LogEventFormatter.Format(@event, writer, OutputTemplate.Default);
            writeLine(writer.ToString());
        }

        public bool IsEnabledFor(LogLevel level) => true;

        public ILog ForContext(string context) => new SourceContextWrapper(this, context);
    }
}