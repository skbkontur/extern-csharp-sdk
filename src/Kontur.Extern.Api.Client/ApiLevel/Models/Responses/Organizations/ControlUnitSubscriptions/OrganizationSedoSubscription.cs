using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations.ControlUnitSubscriptions;

[PublicAPI]
public class OrganizationSedoSubscription: OrganizationControlUnitSubscription
{
    /// <summary>
    /// True, если RegistrationNumber это РНС СФР, иначе это РНС ФСС
    /// </summary>
    public bool IsSfr { get; set; }
    /// <summary>
    /// Регистрационный номер ФСС/СФР
    /// </summary>
    public string RegistrationNumber { get; set; }
    /// <summary>
    /// Статус подписки
    /// </summary>
    public SubscriptionStatus SubscriptionStatus { get; set; }
    /// <summary>
    /// Статус верификации, связанной с этой подпиской
    /// </summary>
    public VerificationStatus VerificationStatus { get; set; }
}