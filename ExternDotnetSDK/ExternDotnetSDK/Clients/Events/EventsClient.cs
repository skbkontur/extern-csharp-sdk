using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KeApiOpenSdk.Clients.Common;
using KeApiOpenSdk.Clients.Common.Logging;
using KeApiOpenSdk.Clients.Common.RequestSenders;
using KeApiOpenSdk.Models.Events;

namespace KeApiOpenSdk.Clients.Events
{
    //todo Сделать нормальные тесты для методов.
    public class EventsClient : IEventsClient
    {
        private readonly InnerCommonClient client;

        public EventsClient(ILogger logger, IRequestSender requestSender) =>
            client = new InnerCommonClient(logger, requestSender);

        public async Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0", TimeSpan? timeout = null) =>
            await client.SendRequestAsync<EventsPage>(
                HttpMethod.Get,
                "/v1/events",
                new Dictionary<string, object>
                {
                    ["take"] = take,
                    ["fromId"] = fromId
                },
                timeout: timeout);
    }
}