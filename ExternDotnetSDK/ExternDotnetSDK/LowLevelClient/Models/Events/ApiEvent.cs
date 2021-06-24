using System;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Events
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