using System;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces.Logging;

namespace ExternDotnetSDK.Clients.Common.DefaultImplementations
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

        public void Log(IHaveHttpResponseMessage response, Exception exc, LogMessageType messageType = LogMessageType.Error)
        {
        }
    }
}