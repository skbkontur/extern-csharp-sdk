using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Docflows.Documents.Requisites
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class BusinessRegistrationDocflowDocumentRequisites : DocflowDocumentRequisites
    {
        /// <summary>
        /// Код документа по справочнику СВДРЕГ
        /// </summary>
        public string SvdregCode { get; set; }
    }
}