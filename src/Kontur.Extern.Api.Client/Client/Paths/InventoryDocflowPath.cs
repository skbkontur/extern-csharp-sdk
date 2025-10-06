using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.Docflows;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ClientDocumentationSection]
    public readonly struct InventoryDocflowPath
    {
        public InventoryDocflowPath(Guid accountId, Guid docflowId, Guid documentId, Guid inventoryId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            InventoryId = inventoryId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public Guid InventoryId { get; }
        public InventoryDocflowDocumentListPath Documents => new(AccountId, DocflowId, DocumentId, InventoryId, Services);
        public IExternClientServices Services { get; }

        public Task<IDocflowWithDocuments?> TryGetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.TryGetInventoryDocflowAsync(AccountId, DocflowId, DocumentId, InventoryId, timeout);
        }

        public Task<IDocflowWithDocuments> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.GetInventoryDocflowAsync(AccountId, DocflowId, DocumentId, InventoryId, timeout);
        }
    }
}