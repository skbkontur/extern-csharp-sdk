using System.Runtime.Serialization;

namespace ExternDotnetSDK.Drafts.Requests
{
    [DataContract]
    public class AdditionalInfoRequest
    {
        /// <summary>
        ///     Тема письма
        /// </summary>
        [DataMember]
        public string Subject { get; set; }

        /// <summary>
        ///     Сертификаты, используемые для подписания
        /// </summary>
        [DataMember]
        public string[] AdditionalCertificates { get; set; }
    }
}