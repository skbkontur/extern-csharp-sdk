using System;
using KeApiOpenSdk.Clients.Common.ResponseMessages;

namespace KeApiOpenSdk.Clients.Common.Logging
{
    public class SilentLogger : ILogger
    {
        public void Log(string message, LogMessageType messageType = LogMessageType.Error)
        {
        }

        public void Log(Exception exc, LogMessageType messageType = LogMessageType.Error)
        {
        }

        public void Log(string message, Exception exc, LogMessageType messageType = LogMessageType.Error)
        {
        }

        public void Log(IResponseMessage response, Exception exc, LogMessageType messageType = LogMessageType.Error)
        {
        }
    }
}