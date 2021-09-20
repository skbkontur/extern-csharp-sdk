using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct DraftBuilderListPath
    {
        public DraftBuilderListPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public DraftBuilderPath WithId(Guid draftBuilderId) => new(AccountId, draftBuilderId, Services);
    }
}