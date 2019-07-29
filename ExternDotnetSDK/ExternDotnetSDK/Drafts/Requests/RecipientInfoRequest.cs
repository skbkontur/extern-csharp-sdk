using System.Runtime.Serialization;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Drafts.Requests
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class RecipientInfoRequest
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