using System;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Documents
{
    public class DemandAttachmentRequisites : DocflowDocumentRequisites
    {
        public string DemandNumber { get; set; }

        [JsonConverter(typeof (DateFormat))]
        public DateTime? DemandDate { get; set; }
        public string[] DemandInnList { get; set; }
    }
}