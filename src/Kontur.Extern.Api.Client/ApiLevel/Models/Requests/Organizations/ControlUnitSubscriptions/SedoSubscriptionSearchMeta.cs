namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Organizations.ControlUnitSubscriptions;

public class SedoSubscriptionSearchMeta
{
    /// <summary>
    /// Регистрационный номер СЭДО/СФР для поиска. При пустом значении - поиск по всем регистрационным номерам
    /// </summary>
    public string? RegistrationNumber { get; set; }
}