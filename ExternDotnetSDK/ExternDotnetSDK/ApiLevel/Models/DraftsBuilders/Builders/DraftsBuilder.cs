using System;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders
{
    public class DraftsBuilder
    {
        public Guid Id { get; set; }
        public DraftsBuilderMeta Meta { get; set; }
        public DraftsBuilderStatus Status { get; set; }
    }
}