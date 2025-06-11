using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Handbooks;

public interface IHandbooksClient
{
    Task<ControlUnitsPage> GetControlUnits(ControlUnitsFilter? filter, TimeSpan? timeout = null);
    Task<ControlUnit> GetControlUnit(string code, AmbiguousUnitType? unitType = null, TimeSpan? timeout = null);
    Task<FnsFormsPage> GetFnsForms(FnsFormsFilter? filter, TimeSpan? timeout = null);
}