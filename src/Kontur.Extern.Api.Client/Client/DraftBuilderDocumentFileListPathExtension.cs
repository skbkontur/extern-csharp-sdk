using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.DocumentFiles;
using Kontur.Extern.Api.Client.Model.DraftBuilders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DraftBuilderDocumentFileListPathExtension
    {
        public static Task<IReadOnlyCollection<DraftsBuilderDocumentFile>> ListAsync(this in DraftBuilderDocumentFileListPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.GetFilesAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, timeout);
        }
    }
}