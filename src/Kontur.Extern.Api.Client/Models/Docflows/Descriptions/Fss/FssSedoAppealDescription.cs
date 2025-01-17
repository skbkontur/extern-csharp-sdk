using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss;

[PublicAPI]
public class FssSedoAppealDescription : FssSedoDescription
{
    /// <summary>
    /// Версия формы документа
    /// </summary>
    public FormVersion? FormVersion { get; set; }

    /// <summary>
    /// Дата формирования сообщения на стороне СФР
    /// </summary>
    public DateTime? MessageDate { get; set; }
}