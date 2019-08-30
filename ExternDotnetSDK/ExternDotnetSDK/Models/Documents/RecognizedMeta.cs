using System;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class RecognizedMeta
    {
        public string DemandNumber { get; set; }

        [JsonConverter(typeof (DateFormat))]
        public DateTime? DemandDate { get; set; }
        public string[] DemandInnList { get; set; }

        public bool IsFullyRecognized(bool withInn) =>
            !string.IsNullOrWhiteSpace(DemandNumber) && DemandDate.HasValue && (!withInn || DemandInnList?.Length > 0);
    }
}