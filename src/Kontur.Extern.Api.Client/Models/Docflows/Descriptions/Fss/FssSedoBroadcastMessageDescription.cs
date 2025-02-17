using System;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss;

public class FssSedoBroadcastMessageDescription : FssSedoDescription
{
    /// <summary>
    /// Версия формы документа
    /// </summary>
    public FormVersion? FormVersion { get; set; }

    /// <summary>
    /// Дата актуальности сообщения
    /// </summary>
    public DateTime? RelevanceDate { get; set; }
}