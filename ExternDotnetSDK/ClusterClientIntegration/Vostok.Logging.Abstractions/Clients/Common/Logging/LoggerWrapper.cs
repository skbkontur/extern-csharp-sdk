using System;
using KeApiOpenSdk.Clients.Common.Logging;
using KeApiOpenSdk.Clients.Common.ResponseMessages;
using Vostok.Logging.Abstractions;

namespace KonturInfrastructureIntegration.Vostok.Logging.Abstractions.Clients.Common.Logging
{
    public class LoggerWrapper : ILogger
    {
        private readonly ILog iLog;
        private readonly Func<IResponseMessage, string> responseToMessageConverter;

        public LoggerWrapper(ILog iLog, Func<IResponseMessage, string> responseToMessageConverter = null)
        {
            this.iLog = iLog;
            this.responseToMessageConverter =
                responseToMessageConverter ?? (x => $"StatusCode: {x.StatusCode} | {x.ReasonPhrase}");
        }

        public void Log(string message, LogMessageType messageType = LogMessageType.Error) =>
            LogEvent(new LogEvent(messageType.ToLogLevel(), DateTimeOffset.Now, message));

        public void Log(Exception exc, LogMessageType messageType = LogMessageType.Error) =>
            LogEvent(new LogEvent(messageType.ToLogLevel(), DateTimeOffset.Now, null, exc));

        public void Log(string message, Exception exc, LogMessageType messageType = LogMessageType.Error) =>
            LogEvent(new LogEvent(messageType.ToLogLevel(), DateTimeOffset.Now, message, exc));

        public void Log(IResponseMessage response, Exception exc, LogMessageType messageType = LogMessageType.Error) =>
            LogEvent(new LogEvent(messageType.ToLogLevel(), DateTimeOffset.Now, responseToMessageConverter(response), exc));

        private void LogEvent(LogEvent logEvent)
        {
            if (iLog.IsEnabledFor(logEvent.Level))
                iLog.Log(logEvent);
        }
    }
}