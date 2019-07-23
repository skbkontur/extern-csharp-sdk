using System;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Documents.Requisites
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class DemandAttachmentRequisites : DocflowDocumentRequisites
    {
        public string DemandNumber { get; set; }

        [JsonConverter(typeof (DateFormat))]
        public DateTime? DemandDate { get; set; }

        public string[] DemandInnList { get; set; }
    }
}