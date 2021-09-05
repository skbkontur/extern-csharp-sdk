using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives.LongOperations;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class DocumentContentPathExtension
    {
        public static ILongOperation<PrintDocumentResult> Print(this in DocumentContentPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            var accountId = path.AccountId;
            var docflowId = path.DocflowId;
            var documentId = path.DocumentId;
            var contentId = path.ContentId;

            return new LongOperation<PrintDocumentResult>(
                () => apiClient.Docflows.StartPrintDocumentAsync(accountId, docflowId, documentId, contentId, timeout),
                taskId => apiClient.Docflows.GetPrintDocumentTaskAsync(accountId, docflowId, documentId, taskId, timeout),
                path.Services.LongOperationsPollingStrategy
            );
        }
    }
}