using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions.Fns.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class BusinessRegistrationDescription : DocflowDescription
    {
        /// <summary>
        /// Код ИФНС
        /// </summary>
        public string Recipient { get; set; }
        
        /// <summary>
        /// ИНН заявителя
        /// </summary>
        public string SenderInn { get; set; }
        
        /// <summary>
        /// Коды СВДРЕГ документов
        /// </summary>
        public string[] SvdRegCodes { get; set; }
        
        /// <summary>
        /// Информация для регистрации
        /// </summary>
        public RegistrationInfoDescription RegistrationInfo { get; set; }

        [PublicAPI]
        [SuppressMessage("ReSharper", "CommentTypo")]
        public class RegistrationInfoDescription
        {
            /// <summary>
            /// Сведения о заявителе
            /// </summary>
            public ApplicantInfoDescription[] ApplicantInfos { get; set; }
            
            /// <summary>
            /// Тип регистрируемой организации
            /// </summary>
            public BusinessType BusinessType { get; set; }
            
            /// <summary>
            /// Информация о регистрируемом ЮЛ
            /// </summary>
            public UlInfoDescription UlInfo { get; set; }
        }
    
        [PublicAPI]
        [SuppressMessage("ReSharper", "CommentTypo")]
        public class ApplicantInfoDescription
        {
            /// <summary>
            /// ФИО
            /// </summary>
            public Fio Fio { get; set; }
            
            /// <summary>
            /// ИНН физического лица заявителя
            /// </summary>
            public string Inn { get; set; }
        }
    
        [PublicAPI]
        [SuppressMessage("ReSharper", "CommentTypo")]
        public class UlInfoDescription
        {
            /// <summary>
            /// Полное наименование юридического лица
            /// </summary>
            public string Name { get; set; }
        }
    }
}