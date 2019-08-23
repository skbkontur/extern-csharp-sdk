using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces.Logging;
using ExternDotnetSDK.Models.Events;

namespace ExternDotnetSDK.Clients.Events
{
    public class EventsClient : IEventsClient
    {
        private readonly InnerCommonClient client;

        public EventsClient(ILogger logger, IRequestSender sender, IRequestFactory requestFactory) =>
            client = new InnerCommonClient(logger, sender, requestFactory);

        public async Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0") =>
            await client.SendRequestAsync<EventsPage>(
                HttpMethod.Get,
                "/v1/events",
                new Dictionary<string, object>
                {
                    ["take"] = take,
                    ["fromId"] = fromId
                });
    }
}