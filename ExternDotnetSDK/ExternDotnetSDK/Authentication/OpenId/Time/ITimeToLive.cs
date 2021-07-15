namespace Kontur.Extern.Client.Authentication.OpenId.Time
{
    internal interface ITimeToLive
    {
        bool HasExpired { get; }

        bool WillExpireAfter(TimeInterval interval);
    }
}