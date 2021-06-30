using System;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Events
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class ApiEvent
    {
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public Urn DocflowType { get; set; }
        public Link DocflowLink { get; set; }
        public Urn NewState { get; set; }
        public DateTime EventDateTime { get; set; }
        public string Id { get; set; }
    }
}