using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Events;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Events;
using Kontur.Extern.Api.Client.Attributes;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Events
{
    [PublicAPI]
    [ClientDocumentationSection]
    public interface IEventsClient
    {
        Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0", TimeSpan? timeout = null);
        public Task ShareEventsAsync(Guid accountId, ShareEventsRequest shareEventsRequest, TimeSpan? timeout = null);
    }
}