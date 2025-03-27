namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Organizations.ControlUnitSubscriptions;

public abstract class ControlUnitSubscriptionSearchRequest
{
    public virtual ControlUnitSubscriptionType ControlUnitSubscriptionType { get; }
    public int? Skip { get; set; }
    public int? Take { get; set; }
}