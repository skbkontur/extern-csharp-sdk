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
    [ApiPathSection]
    public readonly struct DraftBuilderDocumentListPath
    {
        public DraftBuilderDocumentListPath(Guid accountId, Guid draftBuilderId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftBuilderId = draftBuilderId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftBuilderId { get; }
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public DraftBuilderDocumentPath WithId(Guid documentId) => new(AccountId, DraftBuilderId, documentId, services);

        public Task<DraftsBuilderDocumentMeta> SetAsync(
            DraftsBuilderDocumentData? data,
            TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            Guid documentId;
            var request = new DraftsBuilderDocumentMetaRequest
            {
                BuilderData = data ?? new UnknownBuilderDocumentData()
            };
            return apiClient.DraftsBuilder.UpdateDocumentMetaAsync(AccountId, DraftBuilderId, documentId, request, timeout);
        }

        public Task<IReadOnlyCollection<DraftsBuilderDocument>> ListAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.DraftsBuilder.GetDocumentsAsync(AccountId, DraftBuilderId, timeout);
        }
    }
}