﻿using System;
using System.Collections.Generic;
using KeApiClientOpenSdk.Models.Common;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace KeApiClientOpenSdk.Models.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class ApiReplyDocument
    {
        public Guid Id { get; set; }
        public byte[] Content { get; set; }
        public byte[] PrintContent { get; set; }
        public string Filename { get; set; }
        public byte[] Signature { get; set; }
        public List<Link> Links { get; set; }
        public Guid DocflowId { get; set; }
        public Guid DocumentId { get; set; }
    }
}