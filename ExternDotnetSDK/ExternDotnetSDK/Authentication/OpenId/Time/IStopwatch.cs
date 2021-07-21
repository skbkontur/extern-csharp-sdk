namespace Kontur.Extern.Client.Authentication.OpenId.Time
{
    public interface IStopwatch
    {
        TimeInterval Elapsed { get; }
    }
}