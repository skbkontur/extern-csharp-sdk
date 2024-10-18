using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json.Converters;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
[JsonConverter(typeof (StringEnumConverter))]
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