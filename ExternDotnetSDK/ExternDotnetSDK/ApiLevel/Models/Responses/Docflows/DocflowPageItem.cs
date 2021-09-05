using System;
using System.Collections.Generic;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Docflows.Descriptions;

namespace Kontur.Extern.Client.ApiLevel.Models.Responses.Docflows
{
    public class DocflowPageItem : IDocflowPageItem
    {
        public DocflowPageItem()
        {
        }

        public DocflowPageItem(
            Guid id,
            Guid organizationId,
            Urn type,
            Urn status,
            Urn successState,
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
            Links = links;
            SendDate = sendDate;
            LastChangeDate = lastChangeDate;
            Description = description;
        }
        
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public DocflowDescription Description { get; set; }
        public Urn Type { get; set; }
        public Urn Status { get; set; }
        public Urn SuccessState { get; set; }
        public List<Link> Links { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime? LastChangeDate { get; set; }
    }
}