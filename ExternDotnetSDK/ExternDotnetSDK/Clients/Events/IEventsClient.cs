using System;
using System.Threading.Tasks;
using KeApiClientOpenSdk.Models.Events;

namespace KeApiClientOpenSdk.Clients.Events
{
    public interface IEventsClient
    {
        Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0", TimeSpan? timeout = null);
    }
}