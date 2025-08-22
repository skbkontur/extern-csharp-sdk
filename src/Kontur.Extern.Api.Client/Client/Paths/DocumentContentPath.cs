using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Primitives.LongOperations;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ClientDocumentationSection]
    public readonly struct DocumentContentPath
    {
        public DocumentContentPath(Guid accountId, Guid docflowId, Guid documentId, Guid contentId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            ContentId = contentId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public Guid ContentId { get; }
        public IExternClientServices Services { get; }

        public ILongOperation<PrintDocumentResult> Print(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            var accountId = AccountId;
            var docflowId = DocflowId;
            var documentId = DocumentId;
            var contentId = ContentId;

            return new LongOperation<PrintDocumentResult>(
                () => apiClient.Docflows.StartPrintDocumentAsync(accountId, docflowId, documentId, contentId, timeout),
                taskId => apiClient.Docflows.GetPrintDocumentTaskAsync(accountId, docflowId, documentId, taskId, timeout),
                Services.LongOperationsPollingStrategy
            );
        }
    }
}