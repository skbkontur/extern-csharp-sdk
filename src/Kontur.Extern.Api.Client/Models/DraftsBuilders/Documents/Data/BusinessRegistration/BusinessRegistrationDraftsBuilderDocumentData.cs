using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Numbers.BusinessRegistration;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class BusinessRegistrationDraftsBuilderDocumentData : DraftsBuilderDocumentData
    {
        public BusinessRegistrationDraftsBuilderDocumentData(SvdregCode svdregCode, Signer[] signers)
        {
            if (svdregCode.IsEmpty)
                throw Errors.ValueShouldNotBeEmpty(nameof(svdregCode));

            if (signers is null)
                throw new ArgumentNullException(nameof(signers));
            
            if (signers.Length == 0)
                throw Errors.ArrayCannotBeEmpty(nameof(signers));
            
            SvdregCode = svdregCode;
            Signers = signers;
        }
        
        /// <summary>
        /// Код СВДРЕГ
        /// </summary>
        public SvdregCode SvdregCode { get; }
        
        /// <summary>
        /// Данные каждого подписанта
        /// </summary>
        public Signer[] Signers { get; }
    }
}