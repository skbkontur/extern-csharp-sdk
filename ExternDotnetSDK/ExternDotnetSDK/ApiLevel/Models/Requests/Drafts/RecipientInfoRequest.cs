using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class RecipientInfoRequest
    {
        /// <summary>
        /// ИФНС
        /// </summary>
        public string IfnsCode { get; set; }

        /// <summary>
        /// МРИ
        /// </summary>
        public string MriCode { get; set; }

        /// <summary>
        /// ТОГС
        /// </summary>
        public string TogsCode { get; set; }

        /// <summary>
        /// УПФР
        /// </summary>
        public string UpfrCode { get; set; }

        /// <summary>
        /// ФСС 
        /// </summary>
        public string FssCode { get; set; }

        /// <summary>
        /// ИФНС для регистрации бизнеса 
        /// </summary>
        public string RegistrationIfnsCode { get; set; }
    }
}