using System;
using System.Linq;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Exceptions;

namespace Kontur.Extern.Api.Client.Model.Drafts
{
    public class DraftMetadata
    {
        private readonly DraftPayer payer;
        private readonly DraftSender sender;
        private readonly DraftRecipient recipient;
        private string? subject;
        private string[]? additionalCertificates;
        private Guid? machineReadableWarrantId;
        private RelatedDocument? relatedDocumentRequest;
        private DraftCreateOptionsRequest? draftCreateOptions;

        public DraftMetadata(DraftPayer payer, DraftSender sender, DraftRecipient recipient)
        {
            this.payer = payer ?? throw new ArgumentNullException(nameof(payer));
            this.sender = sender ?? throw new ArgumentNullException(nameof(sender));
            this.recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
        }

        public DraftMetadata WithSubject(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(value));
            subject = value;
            return this;
        }

        public DraftMetadata WithAdditionalCertificates(string[] certificates)
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

        public DraftMetadata WithRelatedDocument(Guid docflowId, Guid documentId)
        {
            relatedDocumentRequest = new RelatedDocument(docflowId, documentId);
            return this;
        }

        public DraftMetadata WithMachineReadableWarrantId(Guid warrantId)
        {
            machineReadableWarrantId = warrantId;
            return this;
        }

        public DraftMetadata WithGenerateWarrant(bool generateWarrant)
        {
            draftCreateOptions ??= new DraftCreateOptionsRequest();
            draftCreateOptions.GenerateWarrant = generateWarrant;
            return this;
        }

        public DraftMetaRequest ToRequest()
        {
            AdditionalInfoRequest? additionalInfoRequest = null;
            if (subject != null || additionalCertificates != null || machineReadableWarrantId != null)
            {
                additionalInfoRequest = new AdditionalInfoRequest
                {
                    Subject = subject,
                    AdditionalCertificates = additionalCertificates,
                    MachineReadableWarrantId = machineReadableWarrantId
                };
            }

            return new DraftMetaRequest
            {
                Payer = payer.ToRequest(),
                Sender = sender.ToRequest(),
                Recipient = recipient.ToRequest(),
                AdditionalInfo = additionalInfoRequest,
                RelatedDocument = relatedDocumentRequest,
                DraftOptions = draftCreateOptions
            };
        }
    }
}