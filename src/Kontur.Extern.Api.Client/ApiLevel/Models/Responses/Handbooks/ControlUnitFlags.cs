using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public class ControlUnitFlags
{
    public bool IsActive { get; set; }
    public bool IsTest { get; set; }
    public bool BusinessRegistration { get; set; }
}