using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.Models.Numbers.BusinessRegistration;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class BusinessRegistrationDescription : DocflowDescription
    {
        /// <summary>
        /// Код ИФНС
        /// </summary>
        public string Recipient { get; set; } = null!;
        
        /// <summary>
        /// ИНН заявителя
        /// </summary>
        public string SenderInn { get; set; } = null!;
        
        /// <summary>
        /// Коды СВДРЕГ документов
        /// </summary>
        public SvdregCode[] SvdRegCodes { get; set; } = null!;
        
        /// <summary>
        /// Информация для регистрации
        /// </summary>
        public RegistrationInfoDescription RegistrationInfo { get; set; } = null!;

        [PublicAPI]
        [SuppressMessage("ReSharper", "CommentTypo")]
        public class RegistrationInfoDescription
        {
            /// <summary>
            /// Сведения о заявителе
            /// </summary>
            public ApplicantInfoDescription[] ApplicantInfos { get; set; } = null!;
            
            /// <summary>
            /// Тип регистрируемой организации
            /// </summary>
            public BusinessType BusinessType { get; set; }
            
            /// <summary>
            /// Информация о регистрируемом ЮЛ
            /// </summary>
            public UlInfoDescription? UlInfo { get; set; }
        }
    
        [PublicAPI]
        [SuppressMessage("ReSharper", "CommentTypo")]
        public class ApplicantInfoDescription
        {
            /// <summary>
            /// ФИО
            /// </summary>
            [JsonPropertyName("fio")]
            public PersonFullName PersonFullName { get; set; } = null!;
            
            /// <summary>
            /// ИНН физического лица заявителя
            /// </summary>
            public Inn Inn { get; set; } = null!;
        }
    
        [PublicAPI]
        [SuppressMessage("ReSharper", "CommentTypo")]
        public class UlInfoDescription
        {
            /// <summary>
            /// Полное наименование юридического лица
            /// </summary>
            public string Name { get; set; } = null!;
        }
    }
}