using System.Threading.Tasks;
using ExternDotnetSDK.Models.Events;
using Refit;

namespace ExternDotnetSDK.Clients.Events
{
    public interface IEventsClientRefit
    {
        [Get("/v1/events?take={take}&fromId={fromId}")]
        Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0");
    }
}