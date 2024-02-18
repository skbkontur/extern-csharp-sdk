using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Check;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.Drafts.Prepare;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class PrepareResult
{
    /// <summary>
    /// Результат проверки черновика
    /// </summary>
    public CheckResultData CheckResult { get; set; }

    /// <summary>
    /// Ссылки для работы с черновиком
    /// </summary>
    public Link[] Links { get; set; }

    /// <summary>
    /// Статус подготовки черновика
    /// </summary>
    public PrepareStatus Status { get; set; }
}
