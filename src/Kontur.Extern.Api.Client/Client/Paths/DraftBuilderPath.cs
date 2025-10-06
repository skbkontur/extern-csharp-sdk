using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.DraftBuilders.Builders;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Model.DraftBuilders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders;
using Kontur.Extern.Api.Client.Primitives.LongOperations;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ClientDocumentationSection]
    public readonly struct DraftBuilderPath
    {
        public DraftBuilderPath(Guid accountId, Guid draftBuilderId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftBuilderId = draftBuilderId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftBuilderId { get; }
        public IExternClientServices Services { get; }

        public DraftBuilderDocumentListPath Documents => new(AccountId, DraftBuilderId, Services);

        public Task<DraftsBuilder> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.DraftsBuilder.GetDraftsBuilderAsync(AccountId, DraftBuilderId, timeout);
        }

        public Task<DraftsBuilder?> TryGetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.DraftsBuilder.TryGetDraftsBuilderAsync(AccountId, DraftBuilderId, timeout);
        }

        public Task<bool> DeleteAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.DraftsBuilder.DeleteDraftsBuilderAsync(AccountId, DraftBuilderId, timeout);
        }

        public Task<DraftsBuilderMeta> UpdateMetadataAsync(DraftsBuilderMetadata metadata, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.DraftsBuilder.UpdateDraftsBuilderMetaAsync(AccountId, DraftBuilderId, metadata.ToRequest(), timeout);
        }

        //todo (ks.savelev Ошибочное название метода? Выполняется сборка/создание черновиков, а не их отправка)
        public ILongOperation<DraftsBuilderBuildResult> Send(bool allowToSendIncorrectPfrReport = false, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            var accountId = AccountId;
            var draftBuilderId = DraftBuilderId;

            return new LongOperation<DraftsBuilderBuildResult>(
                () => apiClient.DraftsBuilder.StartBuildDraftsAsync(accountId, draftBuilderId, timeout),
                taskId => apiClient.DraftsBuilder.GetBuildDraftsTaskAsync(accountId, draftBuilderId, taskId, timeout),
                Services.LongOperationsPollingStrategy
            );
        }
    }
}