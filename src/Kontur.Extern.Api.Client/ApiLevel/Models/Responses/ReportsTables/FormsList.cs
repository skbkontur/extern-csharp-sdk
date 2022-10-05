using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.ReportsTables;

[PublicAPI]
public class FormsList
{
    /// <summary>
    /// Список форм
    /// </summary>
    public FormInfo[] Forms { get; set; }
}