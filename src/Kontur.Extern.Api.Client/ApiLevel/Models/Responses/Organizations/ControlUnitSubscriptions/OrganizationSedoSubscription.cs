using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations.ControlUnitSubscriptions;

[PublicAPI]
public class OrganizationSedoSubscription: OrganizationControlUnitSubscription
{
    public bool IsSfr { get; set; }
    public string RegistrationNumber { get; set; }
    public SubscriptionStatus SubscriptionStatus { get; set; }
    public VerificationStatus VerificationStatus { get; set; }
}