using System;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Common.Logging;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Common.ResponseMessages;

namespace Kontur.Extern.Api.Client.UnitTests.Fakes.Logging
{
    internal class TestLogger : ILogger
    {
        public void Log(string message, LogMessageType messageType = LogMessageType.Error)
        {
            throw new NotImplementedException();
        }

        public void Log(Exception exc, LogMessageType messageType = LogMessageType.Error)
        {
            throw new NotImplementedException();
        }

        public void Log(string message, Exception exc, LogMessageType messageType = LogMessageType.Error)
        {
            throw new NotImplementedException();
        }

        public void Log(IResponseMessage response, Exception exc, LogMessageType messageType = LogMessageType.Error)
        {
            throw new NotImplementedException();
        }
    }
}