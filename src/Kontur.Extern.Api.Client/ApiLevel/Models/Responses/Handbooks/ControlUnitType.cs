using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public enum ControlUnitType
{
    Unknown = -1,
    Fns,
    Fss,
    Fst,
    Pfr,
    Rtn,
    Stat
}