﻿using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss;

[PublicAPI]
public class FssSedoProactiveSalaryNotificationDescription : FssSedoDescription
{
    /// <summary>
    /// Версия формы документа
    /// </summary>
    public FormVersion? FormVersion { get; set; }
}