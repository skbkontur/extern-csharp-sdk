namespace ExternDotnetSDK.Logging
{
    /// <summary>
    /// Use it if default ILog functionality is not enough for you.
    /// </summary>
    public interface IILogWrapper : ILog
    {
        ILog Log { get; }
    }
}