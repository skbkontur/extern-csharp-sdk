﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.DraftsBuilders.Info
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class IpInfo
    {
        [Required]
        [DataMember]
        public string OgrnIp { get; set; }
    }
}