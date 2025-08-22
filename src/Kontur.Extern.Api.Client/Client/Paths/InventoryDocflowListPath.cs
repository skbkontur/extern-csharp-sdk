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
    [ClientDocumentationSection]
    public readonly struct InventoryDocflowListPath
    {
        public InventoryDocflowListPath(Guid accountId, Guid docflowId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public IExternClientServices Services { get; }

        public InventoryDocflowPath WithId(Guid inventoryId) => new(AccountId, DocflowId, DocumentId, inventoryId, Services);

        public IEntityList<IDocflow> List(DocflowFilterBuilder? filterBuilder = null)
        {
            return DocflowListsHelper.DocflowsList(
                Services.Api,
                AccountId,
                DocflowId,
                DocumentId,
                filterBuilder,
                (apiClient, accountId, relatedDocflowId, relatedDocumentId, filter, tm) => apiClient.Docflows.GetInventoryDocflowsAsync(accountId, relatedDocflowId, relatedDocumentId, filter, tm)
            );
        }
    }
}