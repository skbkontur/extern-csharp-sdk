using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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

        [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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
        public enum BusinessType
        {
            Unknown,
            Ip,
            Ul
        }
    
        [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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
    
        [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
        public class UlInfoDescription
        {
            /// <summary>
            /// Полное наименование юридического лица
            /// </summary>
            public string Name { get; set; }
        }
    }
}