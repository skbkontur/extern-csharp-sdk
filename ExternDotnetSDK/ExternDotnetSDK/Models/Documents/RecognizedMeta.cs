using System;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class RecognizedMeta
    {
        public string DemandNumber { get; set; }

        [JsonConverter(typeof (DateFormat))]
        public DateTime? DemandDate { get; set; }
        public string[] DemandInnList { get; set; }

        public bool IsFullyRecognized(bool withInn) => !string.IsNullOrWhiteSpace(DemandNumber) &&
                                                       DemandDate.HasValue &&
                                                       (!withInn || DemandInnList?.Length > 0);
    }
}