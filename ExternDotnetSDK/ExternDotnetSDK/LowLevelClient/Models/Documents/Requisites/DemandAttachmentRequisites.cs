using System;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Documents.Requisites
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