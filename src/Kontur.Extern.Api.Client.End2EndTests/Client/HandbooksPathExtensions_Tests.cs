using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Api.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Api.Client.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.Client;

public class HandbooksPathExtensions_Tests : GeneratedAccountTests
{
    private const string Knd = "1151001";

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
        var filter = new ControlUnitsFilter
        {
            Region = "28",
            Type = ControlUnitType.Pfr,
            Skip = 5,
            Take = 50
        };
        var controlUnitsPage = await Context.Handbooks.GetControlUnits(filter);
        controlUnitsPage.ControlUnits.All(x => x.Region == filter.Region && x.Type == filter.Type).Should().BeTrue();
        controlUnitsPage.Take.Should().BeGreaterThan(0);
        controlUnitsPage.Skip.Should().Be(filter.Skip);
    }

    [Fact]
    public async Task Should_return_empty_array_when_skip_greater_than_total_count()
    {
        var filter = new ControlUnitsFilter
        {
            Region = "33",
            Skip = 1600,
            Take = 50
        };
        var controlUnitsPage = await Context.Handbooks.GetControlUnits(filter);
        controlUnitsPage.ControlUnits.All(x => x.Region == filter.Region).Should().BeTrue();
        controlUnitsPage.TotalCount.Should().BeGreaterThan(0);
        controlUnitsPage.Take.Should().Be(0);
        controlUnitsPage.Skip.Should().Be(filter.Skip);
    }

    [Fact]
    public async Task Should_return_control_units_with_inactive()
    {
        var filter = new ControlUnitsFilter
        {
            IncludeInactive = true
        };
        var controlUnitsPage = await Context.Handbooks.GetControlUnits();
        var controlUnitsPageWithInactive = await Context.Handbooks.GetControlUnits(filter);
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
        var fnsFormsPage = await Context.Handbooks.GetFnsForms(new FnsFormsFilter
        {
            Skip = 5,
            Take = 70
        });
        fnsFormsPage.FnsForms.Length.Should().Be(70);
        fnsFormsPage.Take.Should().Be(70);
        fnsFormsPage.Skip.Should().Be(5);
    }

    [Fact]
    public async Task Should_return_empty_fns_forms_when_skip_is_more_than_total_count()
    {
        var fnsFormsPage = await Context.Handbooks.GetFnsForms(new FnsFormsFilter
        {
            Skip = 2000,
            Take = 50
        });
        fnsFormsPage.FnsForms.Length.Should().Be(0);
        fnsFormsPage.Take.Should().Be(0);
        fnsFormsPage.Skip.Should().Be(2000);
    }

    [Fact]
    public async Task Should_return_fns_forms_with_knd()
    {
        var fnsForms = await Context.Handbooks.GetFnsForms(new FnsFormsFilter
        {
            Knd = Knd
        });
        fnsForms.FnsForms.All(x => x.Knd == Knd).Should().BeTrue();
        fnsForms.Skip.Should().Be(0);
    }

    [Fact]
    public async Task Should_return_fns_forms_with_all_filters()
    {
        var fnsForms = await Context.Handbooks.GetFnsForms(new FnsFormsFilter
        {
            Knd = Knd,
            Skip = 5,
            Take = 10
        });
        fnsForms.FnsForms.All(x => x.Knd == Knd).Should().BeTrue();
        fnsForms.FnsForms.Length.Should().Be(10);
        fnsForms.Skip.Should().Be(5);
        fnsForms.Take.Should().Be(10);
    }

    [Fact]
    public async Task Should_return_mvd_unit_by_code()
    {
        var code = "020-015";
        var controlUnit = await Context.Handbooks.GetControlUnit(code, AmbiguousUnitType.Mvd);
        controlUnit.Code.Should().Be(code);
        controlUnit.Type.Should().Be(ControlUnitType.Mvd);
    }
    
    [Fact]
    public async Task Should_return_mvd_control_units()
    {
        var handbookFilter = new ControlUnitsFilter
        {
            Region = "02",
            Type = ControlUnitType.Mvd,
            Skip = 0,
            Take = 5
        };
        var controlUnitList = await Context.Handbooks.GetControlUnits(handbookFilter);
        controlUnitList.ControlUnits.All(x => x.Region == handbookFilter.Region 
                                              && x.Type == handbookFilter.Type 
                                              && x.Flags.IsActive 
                                              && !x.Flags.IsTest 
                                              && !x.Flags.BusinessRegistration).Should().BeTrue();
        controlUnitList.Take.Should().BeGreaterThan(0);
        controlUnitList.Skip.Should().Be(handbookFilter.Skip);
    }
    
    [Fact]
    public async Task Should_return_citizenship_handbook()
    {
        var handbookFilter = new HandbookFilter {Skip = 0, Take = 1000};

        var citizenshipHandbook = await Context.Handbooks.GetHandbook(HandbookType.MvdCitizenship, handbookFilter);
        citizenshipHandbook.HandbookType.Should().Be(HandbookType.MvdCitizenship);
        citizenshipHandbook.Handbook.Any(c => ((MvdCitizenship)c).Name == "Россия").Should().BeTrue();
    }
    
    [Fact]
    public async Task Should_return_rfRegions_handbook()
    {
        var citizenshipHandbook = await Context.Handbooks.GetHandbook(HandbookType.MvdRegionsRf);
        citizenshipHandbook.HandbookType.Should().Be(HandbookType.MvdRegionsRf);
        citizenshipHandbook.Handbook.Any(c => ((MvdRfRegions)c).Name == "Свердловская область").Should().BeTrue();
    }
    
    [Fact]
    public async Task Should_return_empty_rfRegions_handbook()
    {
        var handbookFilter = new HandbookFilter {Skip = 0, Take = 0};

        var citizenshipHandbook = await Context.Handbooks.GetHandbook(HandbookType.MvdRegionsRf, handbookFilter);
        citizenshipHandbook.HandbookType.Should().Be(HandbookType.MvdRegionsRf);
        citizenshipHandbook.Handbook.Should().BeEmpty();
    }
}