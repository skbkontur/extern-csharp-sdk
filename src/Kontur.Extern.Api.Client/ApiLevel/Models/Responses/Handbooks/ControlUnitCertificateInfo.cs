using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public class ControlUnitCertificateInfo
{
    /// <summary>
    /// Контент в формате base64
    /// </summary>
    [UsedImplicitly]
    public string Content { get; set; }

    /// <summary>
    /// Отпечаток сертификата
    /// </summary>
    public string Thumbprint { get; set; }
}