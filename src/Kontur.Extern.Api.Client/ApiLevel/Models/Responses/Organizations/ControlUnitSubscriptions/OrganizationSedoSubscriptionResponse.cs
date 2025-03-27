using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations.ControlUnitSubscriptions;

[PublicAPI]
public class OrganizationSedoSubscriptionResponse
{
    public OrganizationSedoSubscription[] Items { get; set; }
    public long TotalCount { get; set; }
}