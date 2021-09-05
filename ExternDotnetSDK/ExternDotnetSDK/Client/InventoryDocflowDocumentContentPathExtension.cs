using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives.LongOperations;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class InventoryDocflowDocumentContentPathExtension
    {
        public static ILongOperation<PrintDocumentResult> Print(this in InventoryDocflowDocumentContentPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            var accountId = path.AccountId;
            var docflowId = path.DocflowId;
            var documentId = path.DocumentId;
            var inventoryId = path.InventoryId;
            var inventoryDocumentId = path.InventoryDocumentId;
            var contentId = path.ContentId;

            return new LongOperation<PrintDocumentResult>(
                () => apiClient.Docflows.StartPrintInventoryDocumentAsync(accountId, docflowId, documentId, inventoryId, inventoryDocumentId, contentId, timeout),
                taskId => apiClient.Docflows.GetPrintInventoryDocumentTaskAsync(accountId, docflowId, documentId, inventoryId, inventoryDocumentId, taskId, timeout),
                path.Services.LongOperationsPollingStrategy
            );
        }
    }
}