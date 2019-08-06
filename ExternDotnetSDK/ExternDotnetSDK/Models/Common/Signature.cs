using System;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ExternDotnetSDK.Models.Common
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Signature
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SignatureCertificateThumbprint { get; set; }
        public Link ContentLink { get; set; }
        public Link[] Links { get; set; }
    }
}