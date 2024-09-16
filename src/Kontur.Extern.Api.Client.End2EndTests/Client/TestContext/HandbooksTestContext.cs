using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestContext;

public class HandbooksTestContext
{
    private readonly IExtern konturExtern;

    public HandbooksTestContext(IExtern konturExtern)
    {
        this.konturExtern = konturExtern;
    }

    public Task<List<ControlUnit>> GetControlUnits(Guid accountId) => konturExtern.Accounts.WithId(accountId).Handbooks.GetControlUnits();
    public Task<ControlUnit> GetControlUnit(Guid accountId, string code) => konturExtern.Accounts.WithId(accountId).Handbooks.GetControlUnit(code);
    public Task<List<FnsForm>> GetFnsForms(Guid accountId) => konturExtern.Accounts.WithId(accountId).Handbooks.GetFnsForms();
}