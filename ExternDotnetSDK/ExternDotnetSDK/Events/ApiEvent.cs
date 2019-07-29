using System;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Events
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