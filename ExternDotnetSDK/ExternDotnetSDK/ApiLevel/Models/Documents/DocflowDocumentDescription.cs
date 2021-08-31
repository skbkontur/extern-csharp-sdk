using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Documents.Requisites;

namespace Kontur.Extern.Client.ApiLevel.Models.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DocflowDocumentDescription : DocflowDocumentDescriptionBase<DocflowDocumentRequisites>
    {
    }
}