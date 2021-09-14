namespace Kontur.Extern.Api.Client.Common.Time
{
    public interface IStopwatch
    {
        TimeInterval Elapsed { get; }
    }
}