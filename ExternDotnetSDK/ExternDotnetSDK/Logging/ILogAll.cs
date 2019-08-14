namespace ExternDotnetSDK.Logging
{
    public interface ILogAll : ILogError, ILogTrace, ILogInfo, ILogWarn, ILogDebug, ILogFatal
    {
    }
}