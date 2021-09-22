using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Paths;
using DraftsBuilderDocumentFile = Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.DraftsBuilderDocumentFile;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DraftBuilderDocumentFileListPathExtension
    {
        public static async Task<DraftsBuilderDocumentFile> SetFileAsync(
            this DraftBuilderDocumentFileListPath path, 
            Kontur.Extern.Api.Client.Model.DraftBuilders.DraftsBuilderDocumentFile file, 
            TimeSpan? uploadTimeout = null, 
            TimeSpan? putTimeout = null)
        {
            var apiClient = path.Services.Api;
            var uploader = path.Services.ContentService;
            var crypt = path.Services.Crypt;
            
            var documentRequest = await file
                .CreateSignedRequestAsync(path.AccountId, uploader, crypt, uploadTimeout).ConfigureAwait(false);
            
            return await apiClient.DraftsBuilder
                .UpdateFileAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, file.FileId, documentRequest, putTimeout).ConfigureAwait(false);
        }
        
        public static Task<IReadOnlyCollection<DraftsBuilderDocumentFile>> ListAsync(this in DraftBuilderDocumentFileListPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.GetFilesAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, timeout);
        }
    }
}