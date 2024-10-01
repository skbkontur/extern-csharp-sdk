using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Handbooks;

public interface IHandbooksClient
{
    Task<List<ControlUnit>> GetControlUnits(HandbookFilter? handbookFilter, TimeSpan? timeout = null);
    Task<ControlUnit> GetControlUnit(string code, TimeSpan? timeout = null);
    Task<List<FnsForm>> GetFnsForms(TimeSpan? timeout = null);
}