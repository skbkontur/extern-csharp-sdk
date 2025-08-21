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
    [ApiPathSection]
    public readonly struct DraftBuilderPath
    {
        public DraftBuilderPath(Guid accountId, Guid draftBuilderId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftBuilderId = draftBuilderId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftBuilderId { get; }
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public DraftBuilderDocumentListPath Documents => new(AccountId, DraftBuilderId, services);

        public Task<DraftsBuilder> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.DraftsBuilder.GetDraftsBuilderAsync(AccountId, DraftBuilderId, timeout);
        }

        public Task<DraftsBuilder?> TryGetAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.DraftsBuilder.TryGetDraftsBuilderAsync(AccountId, DraftBuilderId, timeout);
        }

        public Task<bool> DeleteAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.DraftsBuilder.DeleteDraftsBuilderAsync(AccountId, DraftBuilderId, timeout);
        }

        public Task<DraftsBuilderMeta> UpdateMetadataAsync(DraftsBuilderMetadata metadata, TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.DraftsBuilder.UpdateDraftsBuilderMetaAsync(AccountId, DraftBuilderId, metadata.ToRequest(), timeout);
        }

        //todo (ks.savelev Ошибочное название метода? Выполняется сборка/создание черновиков, а не их отправка)
        public ILongOperation<DraftsBuilderBuildResult> Send(bool allowToSendIncorrectPfrReport = false, TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            var accountId = AccountId;
            var draftBuilderId = DraftBuilderId;

            return new LongOperation<DraftsBuilderBuildResult>(
                () => apiClient.DraftsBuilder.StartBuildDraftsAsync(accountId, draftBuilderId, timeout),
                taskId => apiClient.DraftsBuilder.GetBuildDraftsTaskAsync(accountId, draftBuilderId, taskId, timeout),
                services.LongOperationsPollingStrategy
            );
        }
    }
}