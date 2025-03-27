namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Organizations.ControlUnitSubscriptions;

public class SedoSubscriptionSearchRequest: ControlUnitSubscriptionSearchRequest
{
    public override ControlUnitSubscriptionType ControlUnitSubscriptionType => ControlUnitSubscriptionType.FssSedo;

    /// <summary>
    /// Регистрационный номер СЭДО или СФР для поиска. При пустом значении - поиск по всем регистрационным номерам.
    /// </summary>
    public string? RegistrationNumber { get; set; }
}