using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Events;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Events;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestContext;

public class EventsTestContext
{
    private readonly IExtern konturExtern;

    public EventsTestContext(IExtern konturExtern)
    {
        this.konturExtern = konturExtern;
    }

    public Task<EventsPage> GetEvents(int take, string fromId = "0_0") => konturExtern.Events.GetEventsAsync(take, fromId);
    public Task ShareEvents(Guid accountId, ShareEventsRequest shareEventsRequest) => konturExtern.Accounts.WithId(accountId).Events.ShareEventsAsync(shareEventsRequest);
}