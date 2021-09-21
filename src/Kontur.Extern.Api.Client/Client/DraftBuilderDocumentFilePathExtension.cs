using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.DocumentFiles;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.Data;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DraftBuilderDocumentFilePathExtension
    {
        public static Task<DraftsBuilderDocumentFile> GetAsync(this in DraftBuilderDocumentFilePath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.GetFileAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, path.FileId, timeout);
        }
        
        public static Task<DraftsBuilderDocumentFile?> TryGetAsync(this in DraftBuilderDocumentFilePath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.TryGetFileAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, path.FileId, timeout);
        }
        
        public static Task<bool> DeleteAsync(this in DraftBuilderDocumentFilePath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.DeleteFileAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, path.FileId, timeout);
        }
        
        public static Task<DraftsBuilderDocumentFileMeta> GetMetaAsync(this in DraftBuilderDocumentFilePath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.GetFileMetaAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, path.FileId, timeout);
        }
        
        public static Task<DraftsBuilderDocumentFileMeta?> TryGetMetaAsync(this in DraftBuilderDocumentFilePath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.TryGetFileMetaAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, path.FileId, timeout);
        }
        
        public static Task<DraftsBuilderDocumentFileMeta> UpdateMetaAsync(this in DraftBuilderDocumentFilePath path, string fileName, DraftsBuilderDocumentFileData? data = null, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.UpdateFileMetaAsync(
                path.AccountId,
                path.DraftBuilderId,
                path.DocumentId,
                path.FileId,
                new DraftsBuilderFileMetaRequest
                {
                    FileName = fileName,
                    BuilderData = data!
                },
                timeout
            );
        }
        
        public static async Task<Signature> GetSignatureAsync(this DraftBuilderDocumentFilePath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return await apiClient.DraftsBuilder.GetSignatureAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, path.FileId, timeout).ConfigureAwait(false);
        }
        
        public static async Task<Signature?> TryGetSignatureAsync(this DraftBuilderDocumentFilePath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            var signatureBytes = await apiClient.DraftsBuilder.TryGetSignatureAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, path.FileId, timeout).ConfigureAwait(false);
            return signatureBytes is null ? null : Signature.FromBytes(signatureBytes);
        }
    }
}