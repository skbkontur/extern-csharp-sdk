using System;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.Builders;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Model.Drafts;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.Model.DraftBuilders
{
    public class DraftsBuilderMetadata
    {
        public static DraftsBuilderMetadata FnsInventoryDraftsBuilder(
            DraftPayer payer,
            DraftSender sender,
            DraftRecipient recipient,
            FnsInventoryDraftsBuilderData data) => new(
            payer,
            sender,
            recipient,
            DraftBuilderType.Fns.Fns534.Inventory,
            data ?? throw new ArgumentNullException(nameof(data))
        );
        
        public static DraftsBuilderMetadata FnsLetterDraftsBuilder(DraftPayer payer, DraftSender sender, DraftRecipient recipient) => new(
            payer,
            sender,
            recipient,
            DraftBuilderType.Fns.Fns534.Letter
        );
        
        public static DraftsBuilderMetadata RosstatLetterDraftsBuilder(DraftPayer payer, DraftSender sender, DraftRecipient recipient) => new(
            payer,
            sender,
            recipient,
            DraftBuilderType.Rosstat.Letter
        );
        
        public static DraftsBuilderMetadata BusinessRegistrationDraftsBuilder(
            DraftPayer payer,
            DraftSender sender,
            DraftRecipient recipient,
            BusinessRegistrationDraftsBuilderData data) => new(
            payer,
            sender,
            recipient,
            DraftBuilderType.Fns.BusinessRegistration.Registration,
            data ?? throw new ArgumentNullException(nameof(data))
        );

        public static DraftsBuilderMetadata PfrReportDraftsBuilder(
            DraftPayer payer,
            DraftSender sender,
            DraftRecipient recipient,
            PfrReportDraftsBuilderData data) => new(
            payer,
            sender,
            recipient,
            DraftBuilderType.Pfr.Report,
            data ?? throw new ArgumentNullException(nameof(data))
        );
        
        public static DraftsBuilderMetadata PfrIosDraftsBuilder(DraftPayer payer, DraftSender sender, DraftRecipient recipient) => new(
            payer,
            sender,
            recipient,
            DraftBuilderType.Pfr.Ios
        );

        public static DraftsBuilderMetadata PfrLetterDraftsBuilder(DraftPayer payer, DraftSender sender, DraftRecipient recipient) => new(
            payer,
            sender,
            recipient,
            DraftBuilderType.Pfr.Letter
        );

        private readonly DraftPayer payer;
        private readonly DraftSender sender;
        private readonly DraftRecipient recipient;
        private readonly DraftBuilderType builderType;
        private readonly DraftsBuilderData? builderData;

        private DraftsBuilderMetadata(
            DraftPayer payer,
            DraftSender sender,
            DraftRecipient recipient,
            DraftBuilderType builderType,
            DraftsBuilderData? builderData = null)
        {
            if (builderType.IsEmpty)
                throw Errors.ValueShouldNotBeEmpty(nameof(builderType));
            
            this.payer = payer ?? throw new ArgumentNullException(nameof(payer));
            this.sender = sender ?? throw new ArgumentNullException(nameof(sender));
            this.recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
            this.builderType = builderType;
            this.builderData = builderData;
        }

        public DraftsBuilderMetaRequest ToRequest() => new(
            sender.ToRequest(),
            payer.ToRequest(),
            recipient.ToRequest(),
            builderType,
            builderData ?? new UnknownDraftsBuilderData()
        );
    }
}