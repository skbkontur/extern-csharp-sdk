using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Helpers;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct InventoryDocflowListPath
    {
        public InventoryDocflowListPath(Guid accountId, Guid docflowId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public InventoryDocflowPath WithId(Guid inventoryId) => new(AccountId, DocflowId, DocumentId, inventoryId, services);

        public IEntityList<IDocflow> List(DocflowFilterBuilder? filterBuilder = null)
        {
            return DocflowListsHelper.DocflowsList(
                services.Api,
                AccountId,
                DocflowId,
                DocumentId,
                filterBuilder,
                (apiClient, accountId, relatedDocflowId, relatedDocumentId, filter, tm) => apiClient.Docflows.GetInventoryDocflowsAsync(accountId, relatedDocflowId, relatedDocumentId, filter, tm)
            );
        }
    }
}