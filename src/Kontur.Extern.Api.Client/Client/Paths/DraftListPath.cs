using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Model.Drafts;
using Kontur.Extern.Api.Client.Models.Drafts;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct DraftListPath
    {
        public DraftListPath(Guid accountId, IExternClientServices services)
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

        public DraftPath WithId(Guid draftId) => new(AccountId, draftId, services);

        public Task<Draft> CreateDraftAsync(DraftMetadata draftMetadata, TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.Drafts.CreateDraftAsync(AccountId, draftMetadata.ToRequest(), timeout);
        }
    }
}