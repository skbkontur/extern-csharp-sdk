using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks;
using Kontur.Extern.Api.Client.Attributes;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Handbooks;

[PublicAPI]
[ClientDocumentationSection]
public interface IHandbooksClient
{
    Task<ControlUnitsPage> GetControlUnits(ControlUnitsFilter? filter, TimeSpan? timeout = null);
    Task<ControlUnit> GetControlUnit(string code, TimeSpan? timeout = null);
    Task<ControlUnit> GetControlUnit(string code, AmbiguousControlUnitType? controlUnitType, TimeSpan? timeout = null);
    Task<FnsFormsPage> GetFnsForms(FnsFormsFilter? filter, TimeSpan? timeout = null);
    Task<HandbookPage> GetHandbook(HandbookType handbookType, HandbookFilter? handbookFilter = null, TimeSpan? timeout = null);
}