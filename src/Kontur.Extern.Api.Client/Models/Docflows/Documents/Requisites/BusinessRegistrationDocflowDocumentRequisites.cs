using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers.BusinessRegistration;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents.Requisites
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class BusinessRegistrationDocflowDocumentRequisites : DocflowDocumentRequisites
    {
        /// <summary>
        /// Код документа по справочнику СВДРЕГ
        /// </summary>
        public SvdregCode SvdregCode { get; set; }
    }
}