using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public enum ControlUnitType
{
    None = 0,
    Fns,
    Fss,
    Fst,
    Pfr,
    Rtn,
    Stat
}