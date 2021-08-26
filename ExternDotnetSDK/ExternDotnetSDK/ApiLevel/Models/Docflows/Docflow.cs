using System;
using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Documents;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    public class Docflow : IDocflow, IDocflowPageItem
    {
        public Docflow()
        {
        }

        public Docflow(
            Guid id,
            Guid organizationId,
            Urn type,
            Urn status,
            Urn successState,
            List<Document> documents,
            List<Link> links,
            DateTime sendDate,
            DateTime? lastChangeDate,
            DocflowDescription description)
        {
            Id = id;
            OrganizationId = organizationId;
            Type = type;
            Status = status;
            SuccessState = successState;
            Documents = documents;
            Links = links;
            SendDate = sendDate;
            LastChangeDate = lastChangeDate;
            Description = description;
        }
        
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Urn Type { get; set; }
        public Urn Status { get; set; }
        public Urn SuccessState { get; set; }
        public List<Document> Documents { get; set; }
        public List<Link> Links { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public DocflowDescription Description { get; set; }
    }
}