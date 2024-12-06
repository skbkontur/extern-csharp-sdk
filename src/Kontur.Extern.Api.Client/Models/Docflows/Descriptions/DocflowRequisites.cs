using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Requisites;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class DocflowRequisites
{
    /// <summary>
    /// Комментарий к документообороту
    /// </summary>
    public Comment? Comment { get; set; }
}