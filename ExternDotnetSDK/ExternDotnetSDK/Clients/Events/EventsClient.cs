using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Events;
using Refit;

namespace ExternDotnetSDK.Clients.Events
{
    public class EventsClient : InnerCommonClient, IEventsClient
    {
        public EventsClient(ILogError logError, HttpClient client)
            : base(logError, client) =>
            ClientRefit = RestService.For<IEventsClientRefit>(client);

        public IEventsClientRefit ClientRefit { get; }

        public async Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0") =>
            await TryExecuteTask(ClientRefit.GetEventsAsync(take, fromId));
    }
}