using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Primitives.LongOperations;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct InventoryDocflowDocumentContentPath
    {
        public InventoryDocflowDocumentContentPath(Guid accountId, Guid docflowId, Guid documentId, Guid inventoryId, Guid inventoryDocumentId, Guid contentId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            InventoryId = inventoryId;
            InventoryDocumentId = inventoryDocumentId;
            ContentId = contentId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public Guid InventoryId { get; }
        public Guid InventoryDocumentId { get; }
        public Guid ContentId { get; }
        public IExternClientServices Services { get; }

        public ILongOperation<PrintDocumentResult> Print(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            var accountId = AccountId;
            var docflowId = DocflowId;
            var documentId = DocumentId;
            var inventoryId = InventoryId;
            var inventoryDocumentId = InventoryDocumentId;
            var contentId = ContentId;

            return new LongOperation<PrintDocumentResult>(
                () => apiClient.Docflows.StartPrintInventoryDocumentAsync(accountId, docflowId, documentId, inventoryId, inventoryDocumentId, contentId, timeout),
                taskId => apiClient.Docflows.GetPrintInventoryDocumentTaskAsync(accountId, docflowId, documentId, inventoryId, inventoryDocumentId, taskId, timeout),
                Services.LongOperationsPollingStrategy
            );
        }
    }
}