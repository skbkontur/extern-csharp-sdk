using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct DocflowPath
    {
        public DocflowPath(Guid accountId, Guid docflowId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public IExternClientServices Services { get; }

        public DocumentListPath Documents => new(AccountId, DocflowId, Services);

        public DocumentsRequestPath DocumentsRequest(Guid requestId) => new(AccountId, DocflowId, requestId, Services);
    }
}