using System.IO;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Abstractions.Wrappers;
using Vostok.Logging.Formatting;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.Testing.Fakes.Logging
{
    public class TestLog : ILog
    {
        private readonly ITestOutputHelper output;

        public TestLog(ITestOutputHelper output) => this.output = output;

        public void Log(LogEvent @event)
        {
            using var writer = new StringWriter(); 
            LogEventFormatter.Format(@event, writer, OutputTemplate.Default);
            output.WriteLine(writer.ToString());
        }

        public bool IsEnabledFor(LogLevel level) => true;

        public ILog ForContext(string context) => new SourceContextWrapper(this, context);
    }
}