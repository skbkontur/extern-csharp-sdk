namespace Kontur.Extern.Client.Auth.OpenId.Time
{
    internal interface ITimeToLive
    {
        bool HasExpired { get; }
        TimeInterval Remaining { get; }

        bool WillExpireAfter(TimeInterval interval);
    }
}