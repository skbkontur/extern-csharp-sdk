#nullable enable
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;

namespace Kontur.Extern.Client.Model.Drafts
{
    public class DraftRecipient
    {
        public static DraftRecipient Ifns(string ifnsCode, string? mriCode = null) =>
            new(new RecipientInfoRequest
            {
                IfnsCode = ifnsCode,
                MriCode = mriCode
            });

        public static DraftRecipient Fss(string fssCode) =>
            new(new RecipientInfoRequest
            {
                FssCode = fssCode
            });
        
        public static DraftRecipient Togs(string togsCode) =>
            new(new RecipientInfoRequest
            {
                TogsCode = togsCode
            });
        
        public static DraftRecipient Upfr(string upfrCode) =>
            new(new RecipientInfoRequest
            {
                UpfrCode = upfrCode
            });

        private readonly RecipientInfoRequest request;

        private DraftRecipient(RecipientInfoRequest request) => this.request = request;

        public RecipientInfoRequest ToRequest() => request;
    }
}