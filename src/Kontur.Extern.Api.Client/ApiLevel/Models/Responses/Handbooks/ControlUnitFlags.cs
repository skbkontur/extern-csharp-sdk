using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public class ControlUnitFlags
{
    /// <summary>
    /// Указывает на активность контролирующего органа
    /// </summary>
    public bool IsActive { get; set; }
    /// <summary>
    /// Указывает на тестовый статус контролирующего органа
    /// </summary>
    public bool IsTest { get; set; }
    /// <summary>
    /// ФНС поддерживает регистрацию бизнеса
    /// </summary>
    public bool BusinessRegistration { get; set; }
}