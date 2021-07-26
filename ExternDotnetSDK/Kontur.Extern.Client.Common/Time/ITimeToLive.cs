namespace Kontur.Extern.Client.Common.Time
{
    public interface ITimeToLive
    {
        bool HasExpired { get; }
        TimeInterval Remaining { get; }

        bool WillExpireAfter(TimeInterval interval);
    }
}