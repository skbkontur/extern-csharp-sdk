using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Api.Client.End2EndTests.TestEnvironment;
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
    public async Task Get_control_units_should_be_success()
    {
        var controlUnitList = await Context.Handbooks.GetControlUnits();
        controlUnitList.Should().NotBeNullOrEmpty();
        foreach (var controlUnit in controlUnitList)
        {
            controlUnit.Code.Should().NotBeNullOrEmpty();
            controlUnit.Name.Should().NotBeNullOrEmpty();
        }
    }

    [Fact]
    public async Task Get_control_units_with_filter_should_be_success()
    {
        var regions = new[] {"14", "15"};
        var types = new[] {"Pfr"};
        var controlUnitList = await Context.Handbooks.GetControlUnits(new HandbookFilter
        {
            Regions = regions,
            Types = types
        });
        controlUnitList.Should().NotBeNullOrEmpty();
        foreach (var controlUnit in controlUnitList)
        {
            controlUnit.Code.Should().NotBeNullOrEmpty();
            controlUnit.Name.Should().NotBeNullOrEmpty();
            regions.Should().Contain(controlUnit.Region);
            types.Should().Contain(controlUnit.Type.ToString());
        }
    }

    [Fact]
    public async Task Get_control_unit_should_be_success()
    {
        var code = "016-028";
        var controlUnit = await Context.Handbooks.GetControlUnit(code);
        controlUnit.Should().NotBeNull();
        controlUnit.Code.Should().Be(code);
        controlUnit.Name.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Get_fns_forms_should_be_success()
    {
        var fnsForms = await Context.Handbooks.GetFnsForms();
        fnsForms.Should().NotBeNullOrEmpty();
        foreach (var fnsForm in fnsForms)
        {
            fnsForm.Name.Should().NotBeNullOrEmpty();
        }
    }
}