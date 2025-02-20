﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions;
using Kontur.Extern.Api.Client.Models.Docflows.Documents;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;

namespace Kontur.Extern.Api.Client.Models.Docflows
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Docflow : IDocflowWithDocuments
    {
        public Docflow(
            Guid id,
            Guid abonentId,
            Guid externUserId,
            Guid organizationId,
            DocflowType type,
            DocflowStatus status,
            DocflowState successState,
            List<Document> documents,
            List<Link> links,
            DateTime sendDateTime,
            DateTime? lastChangeDateTime,
            DocflowDescription description)
            : this(
                id,
                organizationId,
                type,
                status,
                successState,
                documents,
                links,
                sendDateTime,
                lastChangeDateTime,
                description)
        {
            AbonentId = abonentId;
            ExternUserId = externUserId;
        }

        public Docflow(
            Guid id,
            Guid abonentId,
            Guid externUserId,
            Guid organizationId,
            DocflowType type,
            DocflowStatus status,
            DocflowState successState,
            List<Link> links,
            DateTime sendDateTime,
            DateTime? lastChangeDateTime,
            DocflowDescription description)
            : this(
                id,
                organizationId,
                type,
                status,
                successState,
                links,
                sendDateTime,
                lastChangeDateTime,
                description)
        {
            AbonentId = abonentId;
            ExternUserId = externUserId;
        }

        public Docflow(
            Guid id,
            Guid organizationId,
            DocflowType type,
            DocflowStatus status,
            DocflowState successState,
            List<Document> documents,
            List<Link> links,
            DateTime sendDateTime,
            DateTime? lastChangeDateTime,
            DocflowDescription description)
        {
            Id = id;
            OrganizationId = organizationId;
            Type = type;
            Status = status;
            SuccessState = successState;
            Documents = documents;
            Links = links;
            SendDateTime = sendDateTime;
            LastChangeDateTime = lastChangeDateTime;
            Description = description;
        }

        public Docflow(
            Guid id,
            Guid organizationId,
            DocflowType type,
            DocflowStatus status,
            DocflowState successState,
            List<Link> links,
            DateTime sendDateTime,
            DateTime? lastChangeDateTime,
            DocflowDescription description)
        {
            Id = id;
            OrganizationId = organizationId;
            Type = type;
            Status = status;
            SuccessState = successState;
            Links = links;
            SendDateTime = sendDateTime;
            LastChangeDateTime = lastChangeDateTime;
            Description = description;
            Documents = new List<Document>();
        }

        public Guid Id { get; set; }
        public Guid AbonentId { get; set; }
        public Guid ExternUserId { get; set; }
        public Guid OrganizationId { get; set; }
        public DocflowType Type { get; set; }
        public DocflowStatus Status { get; set; }
        public DocflowState SuccessState { get; set; }
        public List<Document> Documents { get; set; }
        public List<Link> Links { get; set; }
        public DateTime SendDateTime { get; set; }
        public DateTime? LastChangeDateTime { get; set; }
        public DocflowDescription Description { get; set; }

        public bool IsEmpty => Description == null! && Id == default && OrganizationId == default && AbonentId == default && ExternUserId == default;
    }
}