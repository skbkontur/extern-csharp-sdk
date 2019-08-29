using System;
using KeApiClientOpenSdk.Models.Common;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Documents
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