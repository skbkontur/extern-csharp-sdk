using System;

namespace ExternDotnetSDK.Clients.Common.ImplementableInterfaces.Logging
{
    public interface ILogger
    {
        void Log(string message, LogMessageType messageType = LogMessageType.Error);
        void Log(Exception exc, LogMessageType messageType = LogMessageType.Error);
        void Log(string message, Exception exc, LogMessageType messageType = LogMessageType.Error);
        void Log(IHaveHttpResponseMessage response, Exception exc, LogMessageType messageType = LogMessageType.Error);
    }
}