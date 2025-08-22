using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.Documents;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ClientDocumentationSection]
    public readonly struct DraftBuilderDocumentListPath
    {
        public DraftBuilderDocumentListPath(Guid accountId, Guid draftBuilderId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftBuilderId = draftBuilderId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftBuilderId { get; }
        public IExternClientServices Services { get; }

        public DraftBuilderDocumentPath WithId(Guid documentId) => new(AccountId, DraftBuilderId, documentId, Services);

        public Task<DraftsBuilderDocumentMeta> SetAsync(
            DraftsBuilderDocumentData? data,
            TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            Guid documentId;
            var request = new DraftsBuilderDocumentMetaRequest
            {
                BuilderData = data ?? new UnknownBuilderDocumentData()
            };
            return apiClient.DraftsBuilder.UpdateDocumentMetaAsync(AccountId, DraftBuilderId, documentId, request, timeout);
        }

        public Task<IReadOnlyCollection<DraftsBuilderDocument>> ListAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.DraftsBuilder.GetDocumentsAsync(AccountId, DraftBuilderId, timeout);
        }
    }
}