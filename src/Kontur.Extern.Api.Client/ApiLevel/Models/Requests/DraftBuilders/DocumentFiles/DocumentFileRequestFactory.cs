using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders;
using Kontur.Extern.Api.Client.Cryptography;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.Data;
using Kontur.Extern.Api.Client.Uploading;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.DocumentFiles
{
    internal static class DocumentFileRequestFactory
    {
        public static async ValueTask<DraftsBuilderFileRequest> CreateRequestAsync(
            Guid accountId,
            string fileName,
            DraftsBuilderDocumentFileData? builderData,
            IDocumentContentUploadStrategy? contentUploadStrategy,
            IContentService uploader,
            ICrypt crypt,
            TimeSpan? uploadTimeout)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(fileName));
            
            Guid? contentId = null;
            Signature? signature = null;
            if (contentUploadStrategy is not null)
            {
                (contentId, signature) = await contentUploadStrategy.UploadAndSignAsync(accountId, uploader, crypt, uploadTimeout).ConfigureAwait(false);
            }

            return new DraftsBuilderFileRequest(
                contentId,
                signature?.ToBase64String().ToString(),
                new DraftsBuilderFileMetaRequest(fileName, builderData)
            );
        }
    }
}