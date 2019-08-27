using System;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Documents.Requisites
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DemandAttachmentRequisites : DocflowDocumentRequisites
    {
        public string DemandNumber { get; set; }

        [JsonConverter(typeof (DateFormat))]
        public DateTime? DemandDate { get; set; }

        public string[] DemandInnList { get; set; }
    }
}