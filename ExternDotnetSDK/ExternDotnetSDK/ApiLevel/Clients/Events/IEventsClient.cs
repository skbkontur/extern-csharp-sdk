using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Events;

namespace Kontur.Extern.Client.ApiLevel.Clients.Events
{
    public interface IEventsClient
    {
        Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0", TimeSpan? timeout = null);
    }
}