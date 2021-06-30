﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DocflowPageItem
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }

        [JsonConverter(typeof (DocflowDescriptionConverter))]
        public DocflowDescription Description { get; set; }

        [UsedImplicitly]
        public Urn Type { get; set; }

        [UsedImplicitly]
        public Urn Status { get; set; }

        [UsedImplicitly]
        public Urn SuccessState { get; set; }

        [UsedImplicitly]
        public List<Link> Links { get; set; }

        [UsedImplicitly]
        public DateTime SendDate { get; set; }

        [UsedImplicitly]
        public DateTime? LastChangeDate { get; set; }
    }
}