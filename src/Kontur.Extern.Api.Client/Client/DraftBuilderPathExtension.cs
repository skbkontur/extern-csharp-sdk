using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.DraftBuilders.Builders;
using Kontur.Extern.Api.Client.Model.DraftBuilders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.Primitives.LongOperations;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DraftBuilderPathExtension
    {
        public static Task<DraftsBuilder> GetAsync(this in DraftBuilderPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.GetDraftsBuilderAsync(path.AccountId, path.DraftBuilderId, timeout);
        }
        
        public static Task<DraftsBuilder?> TryGetAsync(this in DraftBuilderPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.TryGetDraftsBuilderAsync(path.AccountId, path.DraftBuilderId, timeout);
        }
        
        public static Task DeleteAsync(this in DraftBuilderPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.DeleteDraftsBuilderAsync(path.AccountId, path.DraftBuilderId, timeout);
        }
        
        public static Task<DraftsBuilderMeta> UpdateMetadataAsync(this in DraftBuilderPath path, DraftsBuilderMetadata metadata, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.UpdateDraftsBuilderMetaAsync(path.AccountId, path.DraftBuilderId, metadata.ToRequest(), timeout);
        }

        public static ILongOperation<DraftsBuilderBuildResult> Send(in this DraftBuilderPath path, bool allowToSendIncorrectPfrReport = false, TimeSpan? timeout = null)
        {   
            var apiClient = path.Services.Api;
            var accountId = path.AccountId;
            var draftBuilderId = path.DraftBuilderId;

            return new LongOperation<DraftsBuilderBuildResult>(
                () => apiClient.DraftsBuilder.StartBuildDraftsAsync(accountId, draftBuilderId, timeout),
                taskId => apiClient.DraftsBuilder.GetBuildDraftsTaskAsync(accountId, draftBuilderId, taskId, timeout),
                path.Services.LongOperationsPollingStrategy
            );
        }
    }
}