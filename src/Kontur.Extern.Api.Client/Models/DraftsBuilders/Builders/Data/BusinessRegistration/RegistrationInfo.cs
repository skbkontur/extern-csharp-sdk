using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns.BusinessRegistration;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class RegistrationInfo
    {
        /// <summary>
        /// Код заявления по справочнику СФРД
        /// </summary>
        public ApplicationCode? ApplicationCode { get; set; }

        /// <summary>
        /// Информация о заявителе
        /// </summary>
        //[Required]
        public ApplicantInfo[] ApplicantInfos { get; set; }
        
        /// <summary>
        /// Тип регистрируемого бизнеса
        /// </summary>
        public BusinessType? BusinessType { get; set; }
        
        /// <summary>
        /// Информация об ИП
        /// </summary>
        public IpInfo IpInfo { get; set; }
        
        /// <summary>
        /// Информация о ЮЛ
        /// </summary>
        public UlInfo UlInfo { get; set; }
    }
}