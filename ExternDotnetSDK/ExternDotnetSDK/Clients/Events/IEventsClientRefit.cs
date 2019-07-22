using System.Threading.Tasks;
using ExternDotnetSDK.Events;
using Refit;

namespace ExternDotnetSDK.Clients.Events
{
    internal interface IEventsClientRefit
    {
        [Get("/v1/events?take={take}&fromId={fromId}")]
        Task<EventsPage> GetEvents(int take, string fromId = "0_0");
    }
}