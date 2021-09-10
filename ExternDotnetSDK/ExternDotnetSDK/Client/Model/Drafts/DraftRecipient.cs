#nullable enable
using System;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Client.Models.Numbers;

namespace Kontur.Extern.Client.Model.Drafts
{
    public class DraftRecipient
    {
        public static DraftRecipient Ifns(IfnsCode ifnsCode, MriCode? mriCode = null)
        {
            if (ifnsCode is null)
                throw new ArgumentNullException(nameof(ifnsCode));
            
            return new(new RecipientInfoRequest
            {
                IfnsCode = ifnsCode.ToString(),
                MriCode = mriCode?.ToString()
            });
        }

        public static DraftRecipient Fss(FssCode fssCode)
        {
            if (fssCode is null)
                throw new ArgumentNullException(nameof(fssCode));
            
            return new(new RecipientInfoRequest
            {
                FssCode = fssCode?.ToString()
            });
        }

        public static DraftRecipient Togs(TogsCode togsCode)
        {
            if (togsCode is null)
                throw new ArgumentNullException(nameof(togsCode));
            
            return new(new RecipientInfoRequest
            {
                TogsCode = togsCode?.ToString()
            });
        }

        public static DraftRecipient Upfr(UpfrCode upfrCode)
        {
            if (upfrCode is null)
                throw new ArgumentNullException(nameof(upfrCode));
            
            return new(new RecipientInfoRequest
            {
                UpfrCode = upfrCode.ToString()
            });
        }

        private readonly RecipientInfoRequest request;

        private DraftRecipient(RecipientInfoRequest request) => this.request = request;

        public RecipientInfoRequest ToRequest() => request;
    }
}