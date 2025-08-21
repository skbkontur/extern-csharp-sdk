using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct InventoryDocflowDocumentListPath
    {
        public InventoryDocflowDocumentListPath(Guid accountId, Guid docflowId, Guid documentId, Guid inventoryId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            InventoryId = inventoryId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public Guid InventoryId { get; }
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public InventoryDocflowDocumentPath WithId(Guid inventoryDocumentId) => new(AccountId, DocflowId, DocumentId, InventoryId, inventoryDocumentId, services);
    }
}