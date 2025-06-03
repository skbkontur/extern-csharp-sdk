namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations.ControlUnitSubscriptions;

public enum SubscriptionStatus
{
    Unknown = -1,
    ///  <summary>
    /// Подписка организации подключена.
    /// </summary>
    Subscribed,
    /// <summary>
    /// Подписка организации отключена.
    /// </summary>
    Unsubscribed,
    /// <summary>
    /// Подписка организации в процессе.
    /// </summary>
    SubscribeInProgress,
    /// <summary>
    /// Отписка организации в процессе.
    /// </summary>
    UnsubscribeInProgress,
    /// <summary>
    /// Не удалось подписаться.
    /// </summary>
    SubscribeFailed,
    /// <summary>
    /// Из СФР долго не приходит результат подписки.
    /// </summary>
    SubscribeWithoutResponse,
    /// <summary>
    /// Из СФР долго не приходит результат отписки.
    /// </summary>
    UnsubscribeWithoutResponse
}