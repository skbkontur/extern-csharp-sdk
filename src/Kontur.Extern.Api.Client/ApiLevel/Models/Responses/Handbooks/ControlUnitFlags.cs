using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
[JsonConverter(typeof (StringEnumConverter))]
public enum ControlUnitFlags
{
    Unknown = -1,
    IsActive,
    IsTest,
    BusinessRegistration
}