﻿using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Docflows
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class FormVersion
    {
        public string Knd { get; set; }
        public string Okud { get; set; }
        public string Version { get; set; }
        public string FormFullname { get; set; }
        public string FormShortname { get; set; }
    }
}