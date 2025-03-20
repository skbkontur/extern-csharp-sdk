namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations.ControlUnitSubscriptions;

public enum SubscriptionStatus
{
    Unknown = -1,
    Subscribed,
    Unsubscribed,
    SubscribeInProgress,
    UnsubscribeInProgress,
    SubscribeFailed,
}