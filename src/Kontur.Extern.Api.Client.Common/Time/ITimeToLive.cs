namespace Kontur.Extern.Api.Client.Common.Time
{
    public interface ITimeToLive
    {
        bool HasExpired { get; }
        TimeInterval Remaining { get; }

        bool WillExpireAfter(TimeInterval interval);
    }
}