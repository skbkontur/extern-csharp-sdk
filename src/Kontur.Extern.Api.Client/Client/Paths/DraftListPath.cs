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
    [ClientDocumentationSection]
    public readonly struct DraftListPath
    {
        public DraftListPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public DraftPath WithId(Guid draftId) => new(AccountId, draftId, Services);

        public Task<Draft> CreateDraftAsync(DraftMetadata draftMetadata, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Drafts.CreateDraftAsync(AccountId, draftMetadata.ToRequest(), timeout);
        }
    }
}