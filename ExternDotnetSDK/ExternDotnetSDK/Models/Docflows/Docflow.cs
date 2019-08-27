using System;
using System.Collections.Generic;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.Documents;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Docflows
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Docflow
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Urn Type { get; set; }
        public Urn Status { get; set; }
        public Urn SuccessState { get; set; }

        [JsonConverter(typeof (DocflowDescriptionConverter))]
        public DocflowDescription Description { get; set; }

        public List<Document> Documents { get; set; }
        public List<Link> Links { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime? LastChangeDate { get; set; }
    }
}