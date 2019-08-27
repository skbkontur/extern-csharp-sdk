using System;
using System.Threading.Tasks;
using KeApiOpenSdk.Models.Events;

namespace KeApiOpenSdk.Clients.Events
{
    public interface IEventsClient
    {
        Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0", TimeSpan? timeout = null);
    }
}