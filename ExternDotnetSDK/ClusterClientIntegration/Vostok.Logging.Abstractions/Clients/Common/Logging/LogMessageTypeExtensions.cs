using KeApiOpenSdk.Clients.Common.Logging;
using Vostok.Logging.Abstractions;

namespace KonturInfrastructureIntegration.Vostok.Logging.Abstractions.Clients.Common.Logging
{
    public static class LogMessageTypeExtensions
    {
        public static LogLevel ToLogLevel(this LogMessageType type)
        {
            switch (type)
            {
                case LogMessageType.Fatal: return LogLevel.Fatal;
                case LogMessageType.Error: return LogLevel.Error;
                case LogMessageType.Warn: return LogLevel.Warn;
                case LogMessageType.Info: return LogLevel.Info;
                case LogMessageType.Debug: return LogLevel.Debug;
                default: return LogLevel.Debug;
            }
        }
    }
}