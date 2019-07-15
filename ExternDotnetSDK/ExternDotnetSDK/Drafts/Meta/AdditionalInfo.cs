using System.Runtime.Serialization;

namespace ExternDotnetSDK.Drafts.Meta
{
    [DataContract]
    public class AdditionalInfo
    {
        /// <summary>
        ///     Тема письма
        /// </summary>
        [DataMember]
        public string Subject { get; set; }
    }
}