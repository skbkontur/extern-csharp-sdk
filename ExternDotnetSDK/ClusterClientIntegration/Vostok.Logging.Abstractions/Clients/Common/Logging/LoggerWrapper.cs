using System;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Common.Logging;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Common.ResponseMessages;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client.Vostok.Vostok.Logging.Abstractions.Clients.Common.Logging
{
    public class LoggerWrapper : ILogger
    {
        private readonly ILog iLog;
        private readonly Func<IResponseMessage, string> converter;

        public LoggerWrapper(ILog iLog, Func<IResponseMessage, string> converter = null)
        {
            this.iLog = iLog;
            this.converter = converter ?? (x => $"StatusCode: {x.StatusCode} | {x.ReasonPhrase}");
        }

        public void Log(string message, LogMessageType messageType = LogMessageType.Error) =>
            LogEvent(new LogEvent(messageType.ToLogLevel(), DateTimeOffset.Now, message));

        public void Log(Exception exc, LogMessageType messageType = LogMessageType.Error) =>
            LogEvent(new LogEvent(messageType.ToLogLevel(), DateTimeOffset.Now, null, exc));

        public void Log(string message, Exception exc, LogMessageType messageType = LogMessageType.Error) =>
            LogEvent(new LogEvent(messageType.ToLogLevel(), DateTimeOffset.Now, message, exc));

        public void Log(IResponseMessage response, Exception exc, LogMessageType messageType = LogMessageType.Error) =>
            LogEvent(new LogEvent(messageType.ToLogLevel(), DateTimeOffset.Now, converter(response), exc));

        private void LogEvent(LogEvent logEvent)
        {
            if (iLog.IsEnabledFor(logEvent.Level))
                iLog.Log(logEvent);
        }
    }
}