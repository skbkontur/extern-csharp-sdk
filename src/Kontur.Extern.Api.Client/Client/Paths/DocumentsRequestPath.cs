using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct DocumentsRequestPath
    {
        public DocumentsRequestPath(Guid accountId, Guid docflowId, Guid requestId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            RequestId = requestId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }

        public Guid DocflowId { get; }

        public Guid RequestId { get; }

        public IExternClientServices Services { get; }
    }
}