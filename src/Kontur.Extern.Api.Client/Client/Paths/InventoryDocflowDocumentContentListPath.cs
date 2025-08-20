using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct InventoryDocflowDocumentContentListPath
    {
        public InventoryDocflowDocumentContentListPath(Guid accountId, Guid docflowId, Guid documentId, Guid inventoryId, Guid inventoryDocumentId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            InventoryId = inventoryId;
            InventoryDocumentId = inventoryDocumentId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public Guid InventoryId { get; }
        public Guid InventoryDocumentId { get; }
        public IExternClientServices Services { get; }

        public InventoryDocflowDocumentContentPath WithId(Guid contentId) => new(AccountId, DocflowId, DocumentId, InventoryId, InventoryDocumentId, contentId, Services);
    }
}