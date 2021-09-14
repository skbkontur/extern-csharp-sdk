using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct InventoryDocflowDocumentPath
    {
        public InventoryDocflowDocumentPath(Guid accountId, Guid docflowId, Guid documentId, Guid inventoryId, Guid inventoryDocumentId, IExternClientServices services)
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
        
        public InventoryDocflowDocumentContentListPath Contents => new(AccountId, DocflowId, DocumentId, InventoryId, InventoryDocumentId, Services);
    }
}