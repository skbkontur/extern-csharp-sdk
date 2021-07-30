#nullable enable
using System;
using System.Linq;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model.Drafts
{
    public class NewDraft
    {
        private readonly NewDraftPayer payer;
        private readonly NewDraftSender sender;
        private readonly DraftRecipient recipient;
        private string? subject;
        private string[]? additionalCertificates;
        private Guid? relatedDocflowId;
        private Guid? relatedDocumentId;

        public NewDraft(NewDraftPayer payer, NewDraftSender sender, DraftRecipient recipient)
        {
            this.payer = payer ?? throw new ArgumentNullException(nameof(payer));
            this.sender = sender ?? throw new ArgumentNullException(nameof(sender));
            this.recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
        }

        public NewDraft WithSubject(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(value));
            subject = value;
            return this;
        }

        public NewDraft WithAdditionalCertificates(string[] certificates)
        {
            if (certificates is null)
                throw new ArgumentNullException(nameof(certificates));
            if (certificates.Length == 0)
                throw Errors.ArrayCannotBeEmpty(nameof(certificates));
            if (certificates.Any(s => string.IsNullOrWhiteSpace(s)))
                throw Errors.StringsCannotContainNullOrWhitespace(nameof(certificates));
            
            additionalCertificates = certificates;
            return this;
        }

        public NewDraft WithRelatedDocument(Guid docflowId, Guid documentId)
        {
            relatedDocflowId = docflowId;
            relatedDocumentId = documentId;
            return this;
        }

        public DraftMetaRequest ToRequest()
        {
            AdditionalInfoRequest? additionalInfoRequest = null;
            if (subject != null || additionalCertificates != null)
            {
                additionalInfoRequest = new AdditionalInfoRequest
                {
                    Subject = subject,
                    AdditionalCertificates = additionalCertificates
                };
            }

            RelatedDocumentRequest? relatedDocumentRequest = null;
            if (relatedDocflowId.HasValue && relatedDocumentId.HasValue)
            {
                relatedDocumentRequest = new RelatedDocumentRequest
                {
                    RelatedDocflowId = relatedDocflowId.Value,
                    RelatedDocumentId = relatedDocumentId.Value
                };
            }

            return new DraftMetaRequest
            {
                Payer = payer.ToRequest(),
                Sender = sender.ToRequest(),
                Recipient = recipient.ToRequest(),
                AdditionalInfo = additionalInfoRequest,
                RelatedDocument = relatedDocumentRequest
            };
        }
    }
}