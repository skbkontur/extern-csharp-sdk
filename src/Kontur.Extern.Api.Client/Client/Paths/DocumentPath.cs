using System;
using System.Threading.Tasks;
using System.Web;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Docflows;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Helpers;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.Documents;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Replies;
using Kontur.Extern.Api.Client.Primitives;
using Microsoft.AspNetCore.JsonPatch;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ClientDocumentationSection]
    public readonly struct DocumentPath
    {
        public DocumentPath(Guid accountId, Guid docflowId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public InventoryDocflowListPath InventoryDocflows => new(AccountId, DocflowId, DocumentId, Services);
        public DocumentContentListPath Contents => new(AccountId, DocflowId, DocumentId, Services);
        public DocumentReplyPath Reply(Guid replyId) => new(AccountId, DocflowId, DocumentId, replyId, Services);
        public IExternClientServices Services { get; }

        public Task<Document?> TryGetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.TryGetDocumentAsync(AccountId, DocflowId, DocumentId, timeout);
        }

        public Task<Document> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.GetDocumentAsync(AccountId, DocflowId, DocumentId, timeout);
        }

        public Task<Document> PatchAsync(JsonPatchDocument<Document> patch, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.PatchDocumentAsync(AccountId, DocflowId, DocumentId, patch, timeout);
        }

        public IEntityList<IDocflow> RelatedDocflowsList(DocflowFilterBuilder? filterBuilder = null)
        {
            return DocflowListsHelper.DocflowsList(
                Services.Api,
                AccountId,
                DocflowId,
                DocumentId,
                filterBuilder,
                (apiClient, accountId, relatedDocflowId, relatedDocumentId, filter, tm) => apiClient.Docflows.GetRelatedDocflows(accountId, relatedDocflowId, relatedDocumentId, filter, tm)
            );
        }

        public Task<ReplyDocument> GenerateReplyAsync(DocumentType documentType, CertificateContent certificate, TimeSpan? timeout = null)
        {
            if (documentType.IsEmpty)
                throw Errors.ValueShouldNotBeEmpty(nameof(documentType));
            var apiClient = Services.Api;
            return apiClient.Replies.GenerateReplyAsync(
                AccountId,
                DocflowId,
                DocumentId,
                documentType.ToUrn(),
                certificate.ToBytes(),
                timeout);
        }

        public Task<ReplyDocument> GenerateReplyAsync(Link link, CertificateContent certificate, TimeSpan? timeout = null)
        {
            if (link is null)
                throw Errors.ValueShouldNotBeEmpty(nameof(link));

            if (link.Rel != LinksRelations.ToReply)
                throw Errors.InappropriateLink(link.Rel, LinksRelations.ToReply, nameof(link));

            var documentTypeParameter = HttpUtility.ParseQueryString(link.Href.Query).Get("documentType");

            if (string.IsNullOrEmpty(documentTypeParameter) || documentTypeParameter.Contains(","))
                throw Errors.InappropriateReplyLink(nameof(link));

            var apiClient = Services.Api;
            return apiClient.Replies.GenerateReplyAsync(
                AccountId,
                DocflowId,
                DocumentId,
                new Urn("document", documentTypeParameter),
                certificate.ToBytes(),
                timeout);
        }

        public Task<SaveDecryptedContentResult> SaveDocumentDecryptedContentAsync(SaveDecryptedContentRequest request, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.SaveDocumentDecryptedContentAsync(AccountId, DocflowId, DocumentId, request, timeout);
        }
    }
}