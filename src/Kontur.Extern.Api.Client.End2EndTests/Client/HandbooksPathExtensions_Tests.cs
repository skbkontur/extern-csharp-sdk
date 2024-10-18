using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Api.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Api.Client.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.Client;

public class HandbooksPathExtensions_Tests : GeneratedAccountTests
{
    public HandbooksPathExtensions_Tests(ITestOutputHelper output, IsolatedAccountEnvironment environment)
        : base(output, environment)
    {
    }

    [Fact]
    public async Task Should_return_control_units()
    {
        var controlUnitsPage = await Context.Handbooks.GetControlUnits();
        controlUnitsPage.ControlUnits.Length.Should().Be(100);
        controlUnitsPage.Take.Should().Be(100);
        controlUnitsPage.Skip.Should().Be(0);
    }

    [Fact]
    public async Task Should_return_control_units_with_filter()
    {
        var handbookFilter = new ControlUnitsFilter
        {
            Region = "28",
            Type = ControlUnitType.Pfr,
            Skip = 5,
            Take = 50
        };
        var controlUnitsPage = await Context.Handbooks.GetControlUnits(handbookFilter);
        controlUnitsPage.ControlUnits.All(x => x.Region == handbookFilter.Region && x.Type == handbookFilter.Type).Should().BeTrue();
        controlUnitsPage.Take.Should().BeGreaterThan(0);
        controlUnitsPage.Skip.Should().Be(handbookFilter.Skip);
    }

    [Fact]
    public async Task Should_return_empty_array_when_skip_greater_than_total_count()
    {
        var handbookFilter = new ControlUnitsFilter
        {
            Region = "33",
            Skip = 1600,
            Take = 50
        };
        var controlUnitsPage = await Context.Handbooks.GetControlUnits(handbookFilter);
        controlUnitsPage.ControlUnits.All(x => x.Region == handbookFilter.Region).Should().BeTrue();
        controlUnitsPage.TotalCount.Should().BeGreaterThan(0);
        controlUnitsPage.Take.Should().Be(0);
        controlUnitsPage.Skip.Should().Be(handbookFilter.Skip);
    }

    [Fact]
    public async Task Should_return_control_units_with_inactive()
    {
        var handbookFilter = new ControlUnitsFilter
        {
            IncludeInactive = true
        };
        var controlUnitsPage = await Context.Handbooks.GetControlUnits();
        var controlUnitsPageWithInactive = await Context.Handbooks.GetControlUnits(handbookFilter);
        controlUnitsPageWithInactive.TotalCount.Should().BeGreaterThan(controlUnitsPage.TotalCount);
    }

    [Fact]
    public async Task Should_return_control_unit_by_code()
    {
        var code = "084-034";
        var controlUnit = await Context.Handbooks.GetControlUnit(code);
        controlUnit.Code.Should().Be(code);
    }

    [Fact]
    public async Task Should_return_fall_when_control_unit_not_exist()
    {
        const string code = "123456";
        var ex = await Assert.ThrowsAsync<ApiException>(
            () => Context.Handbooks.GetControlUnit(code)
        );
        ex.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_return_fns_forms()
    {
        var fnsFormsPage = await Context.Handbooks.GetFnsForms();
        fnsFormsPage.FnsForms.Length.Should().Be(100);
        fnsFormsPage.Take.Should().Be(100);
        fnsFormsPage.Skip.Should().Be(0);
    }

    [Fact]
    public async Task Should_return_fns_forms_with_filter()
    {
        var fnsFormsPage = await Context.Handbooks.GetFnsForms(skip: 5, take: 70);
        fnsFormsPage.FnsForms.Length.Should().Be(70);
        fnsFormsPage.Take.Should().Be(70);
        fnsFormsPage.Skip.Should().Be(5);
    }

    [Fact]
    public async Task Should_return_empty_fns_forms_when_skip_is_more_than_total_count()
    {
        var fnsFormsPage = await Context.Handbooks.GetFnsForms(skip: 2000, take: 50);
        fnsFormsPage.FnsForms.Length.Should().Be(0);
        fnsFormsPage.Take.Should().Be(0);
        fnsFormsPage.Skip.Should().Be(2000);
    }
}