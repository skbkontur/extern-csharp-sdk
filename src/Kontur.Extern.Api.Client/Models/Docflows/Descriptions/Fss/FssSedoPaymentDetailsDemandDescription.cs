using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss;

[PublicAPI]
public class FssSedoPaymentDetailsDemandDescription
{
    /// <summary>
    /// Версия формы документа
    /// </summary>
    public FormVersion? FormVersion { get; set; }

    /// <summary>
    /// Дедлайн ответа на требование
    /// </summary>
    public DateOnly? ReplyDeadlineDate { get; set; }
}