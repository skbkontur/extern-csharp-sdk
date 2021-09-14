using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Requisites;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DocflowDocumentDescription : DocflowDocumentDescriptionBase<DocflowDocumentRequisites>
    {
    }
}