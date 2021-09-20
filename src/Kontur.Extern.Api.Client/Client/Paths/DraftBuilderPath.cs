using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct DraftBuilderPath
    {
        public DraftBuilderPath(Guid accountId, Guid draftBuilderId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftBuilderId = draftBuilderId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftBuilderId { get; }
        public IExternClientServices Services { get; }
    }
}