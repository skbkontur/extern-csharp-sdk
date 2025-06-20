using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestContext;

public class HandbooksTestContext
{
    private readonly IExtern konturExtern;

    public HandbooksTestContext(IExtern konturExtern)
    {
        this.konturExtern = konturExtern;
    }

    public Task<ControlUnitsPage> GetControlUnits(ControlUnitsFilter? filter = null) => konturExtern.Accounts.Handbooks.GetControlUnits(filter);
    public Task<ControlUnit> GetControlUnit(string code, AmbiguousUnitType? unitType = null) => konturExtern.Accounts.Handbooks.GetControlUnit(code, unitType);
    public Task<FnsFormsPage> GetFnsForms(FnsFormsFilter? fnsFormsFilter = null) => konturExtern.Accounts.Handbooks.GetFnsForms(fnsFormsFilter);
    public Task<HandbookPage> GetHandbook(HandbookType handbookType, HandbookFilter? handbookFilter = null) => konturExtern.Accounts.Handbooks.GetHandbook(handbookType, handbookFilter);
}