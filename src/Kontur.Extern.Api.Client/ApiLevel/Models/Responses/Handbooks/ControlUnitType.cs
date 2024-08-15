using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public enum ControlUnitType
{
    Unknown = -1,
    Cz,
    Fms,
    Fns,
    Fss,
    Fst,
    Pfr,
    Rtn,
    Stat,
    Trade
}