using System;
using System.Linq;
using Kontur.Extern.Client.ApiLevel.Clients.Common.Logging;
using Kontur.Extern.Client.ApiLevel.Clients.Common.ResponseMessages;

namespace Kontur.Extern.Client.Vostok.Kontur.Logging.Clients.Common.Logging
{
    public class LoggerWrapper : ILogger
    {
        private readonly ILog iLog;
        private readonly Type iLogType;
        private readonly Func<IResponseMessage, string> converter;

        public LoggerWrapper(ILog iLog, Func<IResponseMessage, string> converter = null)
        {
            this.iLog = iLog;
            iLogType = iLog.GetType();
            this.converter = converter ?? (x => $"StatusCode: {x.StatusCode} | {x.ReasonPhrase}");
        }

        public void Log(string message, LogMessageType messageType = LogMessageType.Error) =>
            Log(messageType, new object[] {message});

        public void Log(Exception exc, LogMessageType messageType = LogMessageType.Error) =>
            Log(messageType, new object[] {exc});

        public void Log(string message, Exception exc, LogMessageType messageType = LogMessageType.Error) =>
            Log(messageType, new object[] {message, exc});

        public void Log(IResponseMessage response, Exception exc, LogMessageType messageType = LogMessageType.Error) =>
            Log(converter(response), exc, messageType);

        private void Log(LogMessageType messageType, object[] methodParameters)
        {
            var methodName = messageType == LogMessageType.Trace ? "Debug" : messageType.ToString();
            var isEnabledInfo = iLogType.GetProperty($"Is{methodName}Enabled", typeof (bool));
            if (isEnabledInfo != null && (bool)isEnabledInfo.GetValue(iLog))
                iLogType.GetMethod(methodName, methodParameters.Select(x => x.GetType()).ToArray())
                    ?.Invoke(iLog, methodParameters);
        }
    }
}