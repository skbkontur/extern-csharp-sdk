namespace Kontur.Extern.Client.Authentication.OpenId.Time
{
    internal interface ITimeToLive
    {
        bool HasExpired { get; }
        TimeInterval Remaining { get; }

        bool WillExpireAfter(TimeInterval interval);
    }
}