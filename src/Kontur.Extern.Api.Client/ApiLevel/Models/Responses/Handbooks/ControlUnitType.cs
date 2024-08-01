using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json.Converters;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
[JsonConverter(typeof (StringEnumConverter))]
public enum ControlUnitType
{
    [EnumMember(Value = "unknown")]
    Unknown = -1,
    [EnumMember(Value = "cz")]
    Cz,
    [EnumMember(Value = "fms")]
    Fms,
    [EnumMember(Value = "fns")]
    Fns,
    [EnumMember(Value = "fss")]
    Fss,
    [EnumMember(Value = "fst")]
    Fst,
    [EnumMember(Value = "pfr")]
    Pfr,
    [EnumMember(Value = "rtn")]
    Rtn,
    [EnumMember(Value = "stat")]
    Stat,
    [EnumMember(Value = "trade")]
    Trade
}