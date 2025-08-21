using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.DocumentsRequests;
using Microsoft.AspNetCore.JsonPatch;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct DocflowPath
    {
        public DocflowPath(Guid accountId, Guid docflowId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public DocumentListPath Documents => new(AccountId, DocflowId, services);

        public DocumentsRequestPath DocumentsRequest(Guid requestId) => new(AccountId, DocflowId, requestId, services);

        public Task<IDocflowWithDocuments> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.Docflows.GetDocflowAsync(AccountId, DocflowId, timeout);
        }

        public Task<IDocflowWithDocuments?> TryGetAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.Docflows.TryGetDocflowAsync(AccountId, DocflowId, timeout);
        }

        public Task<IDocflowWithDocuments> PatchAsync(JsonPatchDocument<IDocflowWithDocuments> patch, TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.Docflows.PatchDocflowAsync(AccountId, DocflowId, patch, timeout);
        }

        public Task<DocumentsRequest> GenerateDocumentsRequestAsync(CertificateContent certificate, TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.Docflows.GenerateDocumentsRequestAsync(AccountId, DocflowId, certificate.ToBytes(), timeout);
        }
    }
}