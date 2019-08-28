using System;
using KeApiOpenSdk.Clients.Common.Logging;
using KeApiOpenSdk.Clients.Common.ResponseMessages;

namespace KonturInfrastructureIntegration.Kontur.Logging.Clients.Common.Logging
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

        public void Log(string message, LogMessageType messageType = LogMessageType.Error)
        {
            if ((messageType == LogMessageType.Debug || messageType == LogMessageType.Trace) && iLog.IsDebugEnabled)
                iLog.Debug(message);
            else if (messageType == LogMessageType.Error && iLog.IsErrorEnabled)
                iLog.Error(message);
            else if (messageType == LogMessageType.Fatal && iLog.IsFatalEnabled)
                iLog.Fatal(message);
            else if (messageType == LogMessageType.Info && iLog.IsInfoEnabled)
                iLog.Info(message);
            else if (messageType == LogMessageType.Warn && iLog.IsWarnEnabled)
                iLog.Warn(message);
        }

        public void Log(Exception exc, LogMessageType messageType = LogMessageType.Error)
        {
            if ((messageType == LogMessageType.Debug || messageType == LogMessageType.Trace) && iLog.IsDebugEnabled)
                iLog.Debug(exc);
            else if (messageType == LogMessageType.Error && iLog.IsErrorEnabled)
                iLog.Error(exc);
            else if (messageType == LogMessageType.Fatal && iLog.IsFatalEnabled)
                iLog.Fatal(exc);
            else if (messageType == LogMessageType.Info && iLog.IsInfoEnabled)
                iLog.Info(exc);
            else if (messageType == LogMessageType.Warn && iLog.IsWarnEnabled)
                iLog.Warn(exc);
        }

        public void Log(string message, Exception exc, LogMessageType messageType = LogMessageType.Error)
        {
            if ((messageType == LogMessageType.Debug || messageType == LogMessageType.Trace) && iLog.IsDebugEnabled)
                iLog.Debug(message, exc);
            else if (messageType == LogMessageType.Error && iLog.IsErrorEnabled)
                iLog.Error(message, exc);
            else if (messageType == LogMessageType.Fatal && iLog.IsFatalEnabled)
                iLog.Fatal(message, exc);
            else if (messageType == LogMessageType.Info && iLog.IsInfoEnabled)
                iLog.Info(message, exc);
            else if (messageType == LogMessageType.Warn && iLog.IsWarnEnabled)
                iLog.Warn(message, exc);
        }

        public void Log(IResponseMessage response, Exception exc, LogMessageType messageType = LogMessageType.Error) =>
            Log(responseToMessageConverter(response), exc, messageType);
    }
}