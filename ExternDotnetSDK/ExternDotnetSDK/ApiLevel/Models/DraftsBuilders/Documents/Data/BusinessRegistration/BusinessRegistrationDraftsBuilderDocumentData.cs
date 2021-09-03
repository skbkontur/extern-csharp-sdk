using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class BusinessRegistrationDraftsBuilderDocumentData : DraftsBuilderDocumentData
    {
        /// <summary>
        /// Код СВДРЕГ
        /// </summary>
        public string SvdregCode { get; set; }
        
        /// <summary>
        /// Данные каждого подписанта
        /// </summary>
        public Signer[] Signers { get; set; }
    }
}