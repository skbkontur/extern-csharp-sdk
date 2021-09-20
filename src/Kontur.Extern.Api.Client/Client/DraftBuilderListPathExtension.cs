using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Model.DraftBuilders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DraftBuilderListPathExtension
    {
        public static Task<DraftsBuilder> CreateDraftBuilderAsync(this in DraftBuilderListPath path, DraftsBuilderMetadata draftsBuilderMetadata, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;

            return apiClient.DraftsBuilder.CreateDraftsBuilderAsync(path.AccountId, draftsBuilderMetadata.ToRequest(), timeout);
        }
    }
}