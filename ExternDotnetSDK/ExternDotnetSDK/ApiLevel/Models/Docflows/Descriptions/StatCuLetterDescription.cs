﻿using System;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class StatCuLetterDescription : DocflowDescription
    {
        public string Cu { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public Guid? RelatedDocflowId { get; set; }
        public string Okpo { get; set; }
    }
}