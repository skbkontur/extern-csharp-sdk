using System;
using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Documents;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    public interface IDocflow : IDocflowDto
    {
        Guid Id { get; }
        Guid OrganizationId { get; }
        Urn Status { get; }
        Urn SuccessState { get; }
        List<Document> Documents { get; }
        List<Link> Links { get; }
        DateTime SendDate { get; }
        DateTime? LastChangeDate { get; }
    }
}