using System;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Documents.Requisites
{
    public class DemandAttachmentRequisites : DocflowDocumentRequisites
    {
        public string DemandNumber { get; set; }

        [JsonConverter(typeof (DateFormat))]
        public DateTime? DemandDate { get; set; }

        public string[] DemandInnList { get; set; }
    }
}