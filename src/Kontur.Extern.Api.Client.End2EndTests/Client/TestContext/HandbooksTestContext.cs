using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestContext;

public class HandbooksTestContext
{
    private readonly IExtern konturExtern;

    public HandbooksTestContext(IExtern konturExtern)
    {
        this.konturExtern = konturExtern;
    }

    public Task<ControlUnitsPage> GetControlUnits(ControlUnitsFilter? handbookFilter = null) => konturExtern.Accounts.Handbooks.GetControlUnits(handbookFilter);
    public Task<ControlUnitsPageItem> GetControlUnit(string code) => konturExtern.Accounts.Handbooks.GetControlUnit(code);
    public Task<FnsFormsPage> GetFnsForms(int skip = 0, int take = 100) => konturExtern.Accounts.Handbooks.GetFnsForms(skip, take);
}