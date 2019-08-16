using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Models.Events;

namespace ExternDotnetSDK.Clients.Events
{
    public interface IEventsClient : IHttpClient
    {
        Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0");
    }
}