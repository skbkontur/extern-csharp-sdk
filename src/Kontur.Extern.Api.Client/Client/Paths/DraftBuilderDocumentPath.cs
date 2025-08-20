using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct DraftBuilderDocumentPath
    {
        public DraftBuilderDocumentPath(Guid accountId, Guid draftBuilderId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftBuilderId = draftBuilderId;
            DocumentId = documentId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftBuilderId { get; }
        public Guid DocumentId { get; }
        public IExternClientServices Services { get; }

        public DraftBuilderDocumentFileListPath Files => new(AccountId, DraftBuilderId, DocumentId, Services);

        public Task<DraftsBuilderDocument> GetAsync(DraftsBuilderDocumentData data, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.DraftsBuilder.GetDocumentAsync(AccountId, DraftBuilderId, DocumentId, timeout);
        }

        public Task<DraftsBuilderDocument?> TryGetAsync(DraftsBuilderDocumentData data, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.DraftsBuilder.TryGetDocumentAsync(AccountId, DraftBuilderId, DocumentId, timeout);
        }

        public Task<DraftsBuilderDocumentMeta> GetMetaAsync(DraftsBuilderDocumentData data, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.DraftsBuilder.GetDocumentMetaAsync(AccountId, DraftBuilderId, DocumentId, timeout);
        }

        public Task<DraftsBuilderDocumentMeta?> TryGetMetaAsync(DraftsBuilderDocumentData data, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.DraftsBuilder.TryGetDocumentMetaAsync(AccountId, DraftBuilderId, DocumentId, timeout);
        }

        public Task<bool> DeleteAsync(DraftsBuilderDocumentData data, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.DraftsBuilder.DeleteDocumentAsync(AccountId, DraftBuilderId, DocumentId, timeout);
        }
    }
}