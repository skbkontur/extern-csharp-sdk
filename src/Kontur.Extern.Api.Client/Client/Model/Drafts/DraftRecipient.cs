using System;
using System.Diagnostics.CodeAnalysis;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Model.Drafts
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftRecipient
    {
        /// <summary>
        /// ИФНС + МРИ
        /// </summary>
        public static DraftRecipient Ifns(IfnsCode ifnsCode, MriCode? mriCode = null)
        {
            if (ifnsCode is null)
                throw new ArgumentNullException(nameof(ifnsCode));
            
            return new(new RecipientInfoRequest
            {
                IfnsCode = ifnsCode,
                MriCode = mriCode
            });
        }

        /// <summary>
        /// ФСС 
        /// </summary>
        public static DraftRecipient Fss(FssCode fssCode)
        {
            if (fssCode is null)
                throw new ArgumentNullException(nameof(fssCode));
            
            return new(new RecipientInfoRequest
            {
                FssCode = fssCode
            });
        }

        /// <summary>
        /// ТОГС
        /// </summary>
        public static DraftRecipient Togs(TogsCode togsCode)
        {
            if (togsCode is null)
                throw new ArgumentNullException(nameof(togsCode));
            
            return new(new RecipientInfoRequest
            {
                TogsCode = togsCode
            });
        }
        
        /// <summary>
        /// УПФР
        /// </summary>
        public static DraftRecipient Upfr(UpfrCode upfrCode)
        {
            if (upfrCode is null)
                throw new ArgumentNullException(nameof(upfrCode));
            
            return new(new RecipientInfoRequest
            {
                UpfrCode = upfrCode
            });
        }
        
        /// <summary>
        /// ИФНС для регистрации бизнеса 
        /// </summary>
        public static DraftRecipient RegistrationIfns(IfnsCode ifnsCode)
        {
            if (ifnsCode is null)
                throw new ArgumentNullException(nameof(ifnsCode));
            
            return new(new RecipientInfoRequest
            {
                RegistrationIfnsCode = ifnsCode
            });
        }

        private readonly RecipientInfoRequest request;

        private DraftRecipient(RecipientInfoRequest request) => this.request = request;

        public RecipientInfoRequest ToRequest() => request;
    }
}