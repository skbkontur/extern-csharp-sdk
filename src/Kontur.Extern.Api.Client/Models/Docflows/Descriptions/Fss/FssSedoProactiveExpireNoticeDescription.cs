using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss;

[PublicAPI]
public class FssSedoProactiveExpireNoticeDescription : FssSedoDescription
{
    /// <summary>
    /// Номер процесса социальной поддержки
    /// </summary>
    public string? SocialAssistNumber { get; set; }

    /// <summary>
    /// Версия формы документа
    /// </summary>
    public FormVersion? FormVersion { get; set; }
}