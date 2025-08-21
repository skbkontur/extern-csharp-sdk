using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.Docflows;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct InventoryDocflowPath
    {
        public InventoryDocflowPath(Guid accountId, Guid docflowId, Guid documentId, Guid inventoryId, IExternClientServices services)
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
        public InventoryDocflowDocumentListPath Documents => new(AccountId, DocflowId, DocumentId, InventoryId, services);
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public Task<IDocflowWithDocuments?> TryGetAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.Docflows.TryGetInventoryDocflowAsync(AccountId, DocflowId, DocumentId, InventoryId, timeout);
        }

        public Task<IDocflowWithDocuments> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.Docflows.GetInventoryDocflowAsync(AccountId, DocflowId, DocumentId, InventoryId, timeout);
        }
    }
}