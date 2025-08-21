using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Model.DraftBuilders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct DraftBuilderListPath
    {
        public DraftBuilderListPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public DraftBuilderPath WithId(Guid draftBuilderId) => new(AccountId, draftBuilderId, services);

        public Task<DraftsBuilder> CreateDraftBuilderAsync(DraftsBuilderMetadata draftsBuilderMetadata, TimeSpan? timeout = null)
        {
            var apiClient = services.Api;

            return apiClient.DraftsBuilder.CreateDraftsBuilderAsync(AccountId, draftsBuilderMetadata.ToRequest(), timeout);
        }
    }
}