using System;
using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    public interface IDocflowPageItem : IDocflowDto
    {
        Guid Id { get; }
        Guid OrganizationId { get; }
        Urn Status { get; }
        Urn SuccessState { get; }
        List<Link> Links { get; }
        DateTime SendDate { get; }
        DateTime? LastChangeDate { get; }
    }
}