using System.Threading.Tasks;
using ExternDotnetSDK.Models.Events;

namespace ExternDotnetSDK.Clients.Events
{
    /// <summary>
    ///     Contains methods for working with events
    /// </summary>
    public interface IEventsClient
    {
        Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0");
    }
}