namespace Kontur.Extern.Client.Authentication.OpenId.Time
{
    internal interface IStopwatch
    {
        TimeInterval Elapsed { get; }
    }
}