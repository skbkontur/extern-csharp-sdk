using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations.ControlUnitSubscriptions;

[PublicAPI]
public class OrganizationSedoSubscription: OrganizationControlUnitSubscription
{
    /// <summary>
    /// Если регистрационный номер является РНС СФР, то вернется значение true. Если РНС ФСС - false.
    /// </summary>
    public bool IsSfr { get; set; }
    /// <summary>
    /// Регистрационный номер ФСС или СФР.
    /// </summary>
    public string RegistrationNumber { get; set; }
    /// <summary>
    /// Статус подписки.
    /// </summary>
    public SubscriptionStatus SubscriptionStatus { get; set; }
}