using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.Logging;
using ExternDotnetSDK.Clients.Common.RequestSenders;
using ExternDotnetSDK.Models.Events;

namespace ExternDotnetSDK.Clients.Events
{
    public class EventsClient : IEventsClient
    {
        private readonly InnerCommonClient client;

        public EventsClient(ILogger logger, IRequestSender requestSender) =>
            client = new InnerCommonClient(logger, requestSender);

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