using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Events;

namespace ExternDotnetSDK.Clients.Events
{
    public class EventsClient : InnerCommonClient, IEventsClient
    {
        public EventsClient(ILogError logError, HttpClient client)
            : base(logError, client)
        {
        }

        public async Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0") =>
            await SendRequestAsync<EventsPage>(
                HttpMethod.Get,
                "/v1/events",
                new Dictionary<string, object>
                {
                    ["take"] = take,
                    ["fromId"] = fromId
                });
    }
}