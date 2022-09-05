using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.ReportsTables;

[PublicAPI]
public class FormsResult
{
    /// <summary>
    /// Список форм
    /// </summary>
    public FormInfo[] Forms { get; set; }
}