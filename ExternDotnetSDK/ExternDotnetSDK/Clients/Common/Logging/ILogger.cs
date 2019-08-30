using System;
using Kontur.Extern.Client.Clients.Common.ResponseMessages;

namespace Kontur.Extern.Client.Clients.Common.Logging
{
    public interface ILogger
    {
        void Log(string message, LogMessageType messageType = LogMessageType.Error);
        void Log(Exception exc, LogMessageType messageType = LogMessageType.Error);
        void Log(string message, Exception exc, LogMessageType messageType = LogMessageType.Error);
        void Log(IResponseMessage response, Exception exc, LogMessageType messageType = LogMessageType.Error);
    }
}