using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Numbers.BusinessRegistration;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Documents.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class BusinessRegistrationDraftsBuilderDocumentData : DraftsBuilderDocumentData
    {
        /// <summary>
        /// Код СВДРЕГ
        /// </summary>
        public SvdregCode SvdregCode { get; set; }
        
        /// <summary>
        /// Данные каждого подписанта
        /// </summary>
        public Signer[] Signers { get; set; }
    }
}