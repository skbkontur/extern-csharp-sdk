using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Events;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Events;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Events
{
    public interface IEventsClient
    {
        Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0", TimeSpan? timeout = null);
        public Task ShareEventsAsync(Guid accountId, ShareEventsRequest shareEventsRequest, TimeSpan? timeout = null);
    }
}