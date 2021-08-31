using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class RecipientInfo
    {
        /// <summary>
        ///  ИФНС
        /// </summary>
        public string IfnsCode { get; set; }

        /// <summary>
        ///  МРИ
        /// </summary>
        public string MriCode { get; set; }

        /// <summary>
        ///  ТОГС
        /// </summary>
        public string TogsCode { get; set; }

        /// <summary>
        ///  УПФР
        /// </summary>
        public string UpfrCode { get; set; }

        /// <summary>
        ///  ФСС 
        /// </summary>
        public string FssCode { get; set; }

        /// <summary>
        ///  ИФНС для регистрации бизнеса 
        /// </summary>
        public string RegistrationIfnsCode { get; set; }
    }
}