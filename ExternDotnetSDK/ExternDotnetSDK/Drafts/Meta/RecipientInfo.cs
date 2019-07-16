using System.Runtime.Serialization;

namespace ExternDotnetSDK.Drafts.Meta
{
    [DataContract]
    public class RecipientInfo
    {
        /// <summary>ИФНС</summary>
        [DataMember]
        public string IfnsCode { get; set; }

        /// <summary>МРИ</summary>
        [DataMember]
        public string MriCode { get; set; }

        /// <summary>ТОГС</summary>
        [DataMember]
        public string TogsCode { get; set; }

        /// <summary>УПФР</summary>
        [DataMember]
        public string UpfrCode { get; set; }

        /// <summary>ФСС</summary>
        [DataMember]
        public string FssCode { get; set; }
    }
}