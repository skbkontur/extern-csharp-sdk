using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Handbooks;

public interface IHandbooksClient
{
    Task<ControlUnitsPage> GetControlUnits(ControlUnitsFilter? handbookFilter, TimeSpan? timeout = null);
    Task<ControlUnitsPageItem> GetControlUnit(string code, TimeSpan? timeout = null);
    Task<FnsFormsPage> GetFnsForms(FnsFormsFilter? fnsFormsFilter, TimeSpan? timeout = null);
}