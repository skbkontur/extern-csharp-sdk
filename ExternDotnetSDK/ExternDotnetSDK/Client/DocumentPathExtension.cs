#nullable enable
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Documents;
using Kontur.Extern.Client.Helpers;
using Kontur.Extern.Client.Model;
using Kontur.Extern.Client.Model.DocflowFiltering;
using Kontur.Extern.Client.Model.Documents;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class DocumentPathExtension
    {
        public static Task<Document?> TryGetAsync(this in DocumentPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.TryGetDocumentAsync(path.AccountId, path.DocflowId, path.DocumentId, timeout);
        }
        
        public static Task<Document> GetAsync(this in DocumentPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.GetDocumentAsync(path.AccountId, path.DocflowId, path.DocumentId, timeout);
        }
        
        public static IEntityList<DocflowPageItem> RelatedDocflowsList(this in DocumentPath path, DocflowFilterBuilder? filterBuilder = null)
        {
            return DocflowListsHelper.DocflowsList(
                path.Services.Api,
                path.AccountId,
                path.DocflowId,
                path.DocumentId,
                filterBuilder,
                (apiClient, accountId, relatedDocflowId, relatedDocumentId, filter, tm) => apiClient.Docflows.GetRelatedDocflows(accountId, relatedDocflowId, relatedDocumentId, filter, tm)
            );
        }

        public static Task<ApiReplyDocument> GenerateReplyAsync(this in DocumentPath path, DocumentType documentType, CertificateContent certificate, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Replies.GenerateReplyAsync(
                path.AccountId,
                path.DocflowId,
                path.DocumentId,
                documentType.ToUrn(),
                certificate.ToBytes(),
                timeout);
        }
    }
}