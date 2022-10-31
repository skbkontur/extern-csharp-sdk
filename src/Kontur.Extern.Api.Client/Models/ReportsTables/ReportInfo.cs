using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables;

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