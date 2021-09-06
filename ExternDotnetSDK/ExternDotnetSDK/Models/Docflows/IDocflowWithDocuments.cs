using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.ApiTasks;
using Kontur.Extern.Client.Models.Docflows.Documents;

namespace Kontur.Extern.Client.Models.Docflows
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public interface IDocflowWithDocuments : IDocflow, IApiTaskResult
    {
        /// <summary>
        /// Список документов в документообороте
        /// </summary>
        List<Document> Documents { get; }
    }
}