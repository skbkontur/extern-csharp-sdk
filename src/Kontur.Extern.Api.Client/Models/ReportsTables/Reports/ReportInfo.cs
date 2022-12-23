using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables.Common;
using Kontur.Extern.Api.Client.Models.ReportsTables.Forms;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class ReportInfo
{
    /// <summary>
    /// Информация о форме
    /// </summary>
    public FormInfo FormInfo { get; set; }
    /// <summary>
    /// Список дедлайнов формы
    /// </summary>
    public DeadlineInfo[] Deadlines { get; set; }
}