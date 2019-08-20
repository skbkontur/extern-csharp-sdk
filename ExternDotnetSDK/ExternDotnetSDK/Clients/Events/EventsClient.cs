using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Authentication;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.SendAsync;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Events;

namespace ExternDotnetSDK.Clients.Events
{
    public class EventsClient : IEventsClient
    {
        private readonly RequestFactory factory;

        public EventsClient(ILogger logger, IHttpSender client, IAuthenticationProvider authenticationProvider) =>
            factory = new RequestFactory(logger, client, authenticationProvider);

        public async Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0") =>
            await factory.SendRequestAsync<EventsPage>(
                HttpMethod.Get,
                "/v1/events",
                new Dictionary<string, object>
                {
                    ["take"] = take,
                    ["fromId"] = fromId
                });
    }
}