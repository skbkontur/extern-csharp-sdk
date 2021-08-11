#nullable enable
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Drafts;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta;
using Kontur.Extern.Client.Model.Documents.Contents;
using Kontur.Extern.Client.Model.Drafts;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Uploading;
using DraftDocument = Kontur.Extern.Client.Model.Drafts.DraftDocument;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class DraftPathExtension
    {
        public static Task<Draft> GetAsync(this in DraftPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.GetDraftAsync(path.AccountId, path.DraftId, timeout);
        }
        
        public static Task<Draft?> TryGetAsync(this in DraftPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.TryGetDraftAsync(path.AccountId, path.DraftId, timeout);
        }
        
        public static Task DeleteAsync(this in DraftPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.DeleteDraftAsync(path.AccountId, path.DraftId, timeout);
        }
        
        public static Task<DraftMeta> UpdateMetadataAsync(this in DraftPath path, DraftMetadata metadata, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.UpdateDraftMetaAsync(path.AccountId, path.DraftId, metadata.ToRequest(), timeout);
        }

        public static async Task<Guid> SetDocumentAsync(
            this DraftPath path,
            DraftDocument document,
            TimeSpan? uploadTimeout = null,
            TimeSpan? putTimeout = null)
        {
            var apiClient = path.Services.Api;
            var uploader = path.Services.ContentService;

            var contentId = await UploadContent(path.AccountId, uploader, document.DocumentContent).ConfigureAwait(false);
            var documentRequest = await document.CreateSignedRequestAsync(contentId, path.Services.Crypt).ConfigureAwait(false);
            var createdDocument = await apiClient.Drafts.UpdateDocumentAsync(path.AccountId, path.DraftId, document.DocumentId, documentRequest, putTimeout).ConfigureAwait(false);
            return createdDocument.Id;

            Task<Guid> UploadContent(Guid accountId, IContentService contentUploader, IDocumentContent content) =>
                content.UploadAsync(contentUploader, accountId, uploadTimeout);
        }
    }
}