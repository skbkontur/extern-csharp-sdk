using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Events;
using Refit;

namespace ExternDotnetSDK.Clients.Events
{
    public class EventsClient : IEventsClient
    {
        public IEventsClientRefit ClientRefit { get; }

        public EventsClient(HttpClient client) => ClientRefit = RestService.For<IEventsClientRefit>(client);

        public async Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0") =>
            await ClientRefit.GetEventsAsync(take, fromId);
    }
}