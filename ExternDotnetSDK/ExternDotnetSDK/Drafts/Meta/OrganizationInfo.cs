using System.Runtime.Serialization;

namespace ExternDotnetSDK.Drafts.Meta
{
    /// <summary>
    ///     Реквизиты, специфичные для ЮЛ
    /// </summary>
    [DataContract]
    public class OrganizationInfo
    {
        /// <summary>
        ///     КПП
        /// </summary>
        [DataMember]
        public string Kpp
        {
            get => kpp;
            set => kpp = value == "" ? null : value;
        }

        private string kpp;
    }
}