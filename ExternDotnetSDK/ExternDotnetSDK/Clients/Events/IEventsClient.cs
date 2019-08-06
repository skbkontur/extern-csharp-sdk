using System.Threading.Tasks;
using ExternDotnetSDK.Models.Events;

namespace ExternDotnetSDK.Clients.Events
{
    public interface IEventsClient
    {
        IEventsClientRefit ClientRefit { get; }

        Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0");
    }
}