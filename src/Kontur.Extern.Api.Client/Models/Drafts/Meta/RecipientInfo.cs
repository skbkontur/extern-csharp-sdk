using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Drafts.Meta
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class RecipientInfo
    {
        /// <summary>
        ///  ИФНС
        /// </summary>
        public IfnsCode? IfnsCode { get; set; }

        /// <summary>
        ///  МРИ
        /// </summary>
        public MriCode? MriCode { get; set; }

        /// <summary>
        ///  ТОГС
        /// </summary>
        public TogsCode? TogsCode { get; set; }

        /// <summary>
        ///  УПФР
        /// </summary>
        public UpfrCode? UpfrCode { get; set; }

        /// <summary>
        ///  ФСС 
        /// </summary>
        public FssCode? FssCode { get; set; }

        /// <summary>
        ///  ИФНС для регистрации бизнеса 
        /// </summary>
        public string? RegistrationIfnsCode { get; set; }
    }
}