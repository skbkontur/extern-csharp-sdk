using System.Threading.Tasks;
using FluentAssertions;
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
        var controlUnitList = await Context.Handbooks.GetControlUnits(AccountId);
        controlUnitList.Should().NotBeNullOrEmpty();
        foreach (var controlUnit in controlUnitList)
        {
            controlUnit.Code.Should().NotBeNullOrEmpty();
            controlUnit.Name.Should().NotBeNullOrEmpty();
        }
    }
    
    [Fact]
    public async Task Get_fns_forms_should_be_success()
    {
        var fnsForms = await Context.Handbooks.GetFnsForms(AccountId);
        fnsForms.Should().NotBeNullOrEmpty();
        foreach (var fnsForm in fnsForms)
        {
            fnsForm.Name.Should().NotBeNullOrEmpty();
        }
    }
}