﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Drafts.Meta
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftMeta
    {
        [DataMember]
        [Required]
        public Sender Sender { get; set; }

        [DataMember]
        [Required]
        public RecipientInfo Recipient { get; set; }

        [DataMember]
        [Required]
        public AccountInfo Payer { get; set; }

        [DataMember]
        public RelatedDocument RelatedDocument { get; set; }

        [DataMember]
        public AdditionalInfo AdditionalInfo { get; set; }
    }
}