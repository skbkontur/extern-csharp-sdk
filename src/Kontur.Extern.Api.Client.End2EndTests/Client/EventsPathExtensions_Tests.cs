using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Events;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestContext;
using Kontur.Extern.Api.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Api.Client.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.Client;

public class EventsPathExtensions_Tests : GeneratedAccountTests
{
    public EventsPathExtensions_Tests(ITestOutputHelper output, IsolatedAccountEnvironment environment)
        : base(output, environment)
    {
    }

    [Fact]
    public async Task GetEvents_should_be_successful()
    {
        var context = new KonturExternTestContext(output, environment.TestData, new Credentials(environment.TestData.UserName, environment.TestData.Password));
        var events = await context.Events.GetEvents(100);

        events.Should().NotBeNull();
    }

    [Fact]
    public async Task ShareEvents_should_be_successful()
    {
        await Context.Events.ShareEvents(AccountId, new ShareEventsRequest {Subscriber = environment.TestData.ClientId});
    }

    [Fact]
    public void ShareEvents_should_be_bad_request_when_subscriber_does_not_exist()
    {
        var apiException = Assert.ThrowsAsync<ApiException>(
            () => Context.Events.ShareEvents(AccountId, new ShareEventsRequest {Subscriber = Guid.NewGuid().ToString()}));

        apiException.Result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public void ShareEvents_should_be_bad_request_when_empty_subscriber()
    {
        var apiException = Assert.ThrowsAsync<ApiException>(
            () => Context.Events.ShareEvents(AccountId, new ShareEventsRequest()));

        apiException.Result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}