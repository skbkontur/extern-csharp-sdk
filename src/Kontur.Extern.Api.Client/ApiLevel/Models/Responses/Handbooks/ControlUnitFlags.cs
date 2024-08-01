using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json.Converters;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
[JsonConverter(typeof (StringEnumConverter))]
public enum ControlUnitFlags
{
    [EnumMember(Value = "unknown")]
    Unknown = -1,
    [EnumMember(Value = "is-active")]
    IsActive,
    [EnumMember(Value = "is-test")]
    IsTest,
    [EnumMember(Value = "gp3-is-on")]
    Gp3On,
    [EnumMember(Value = "pfr-ios-is-on")]
    PfrIosIsOn,
    [EnumMember(Value = "pfr-ios-fio-snils-is-on")]
    PfrIosFioSnilsIsOn,
    [EnumMember(Value = "pfr-pension-is-on")]
    PfrPensionIsOn,
    [EnumMember(Value = "pfr-letters-only")]
    PfrLettersOnly,
    [EnumMember(Value = "fns-letters-only")]
    FnsLettersOnly,
    [EnumMember(Value = "fns-demands-only")]
    FnsDemandsOnly,
    [EnumMember(Value = "fns-submissions-only")]
    FnsSubmissionsOnly,
    [EnumMember(Value = "gost-2012")]
    Gost2012,
    [EnumMember(Value = "cemp-is-on")]
    CempIsOn,
    [EnumMember(Value = "business-registration")]
    BusinessRegistration
}