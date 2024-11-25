using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class DraftCreateOptionsRequest
{
    /// <summary>
    ///     При создании черновика автоматически добавляется Информационное сообщение о представительстве
    /// </summary>
    public bool GenerateWarrant { get; set; }
}