using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public enum ControlUnitFlags
{
    Unknown = -1,
    IsActive,
    IsTest,
    BusinessRegistration
}