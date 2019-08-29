﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using KeApiClientOpenSdk.Models.Common;
using KeApiClientOpenSdk.Models.DraftsBuilders.Documents.Data;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.DraftsBuilders.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderDocumentMeta
    {
        [Required]
        [DataMember]
        public Urn BuilderType { get; set; }

        [Required]
        [DataMember]
        public DraftsBuilderDocumentData BuilderData { get; set; }
    }
}