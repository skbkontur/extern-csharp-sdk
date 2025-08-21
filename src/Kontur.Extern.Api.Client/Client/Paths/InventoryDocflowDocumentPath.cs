using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct InventoryDocflowDocumentPath
    {
        public InventoryDocflowDocumentPath(Guid accountId, Guid docflowId, Guid documentId, Guid inventoryId, Guid inventoryDocumentId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            InventoryId = inventoryId;
            InventoryDocumentId = inventoryDocumentId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public Guid InventoryId { get; }
        public Guid InventoryDocumentId { get; }
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public InventoryDocflowDocumentContentListPath Contents => new(AccountId, DocflowId, DocumentId, InventoryId, InventoryDocumentId, services);
    }
}