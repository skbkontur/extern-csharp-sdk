namespace Kontur.Extern.Client.Auth.OpenId.Time
{
    public interface IStopwatch
    {
        TimeInterval Elapsed { get; }
    }
}