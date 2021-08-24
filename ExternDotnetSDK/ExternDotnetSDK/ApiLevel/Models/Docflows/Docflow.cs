using System;
using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Documents;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    public class Docflow : IDocflowDto
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Urn Type { get; set; }
        public Urn Status { get; set; }
        public Urn SuccessState { get; set; }
        public DocflowDescription Description { get; set; }
        public List<Document> Documents { get; set; }
        public List<Link> Links { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime? LastChangeDate { get; set; }
    }
}