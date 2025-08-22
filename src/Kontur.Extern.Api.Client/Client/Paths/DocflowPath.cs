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
    [ClientDocumentationSection]
    public readonly struct DocflowPath
    {
        public DocflowPath(Guid accountId, Guid docflowId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public IExternClientServices Services { get; }

        public DocumentListPath Documents => new(AccountId, DocflowId, Services);

        public DocumentsRequestPath DocumentsRequest(Guid requestId) => new(AccountId, DocflowId, requestId, Services);

        public Task<IDocflowWithDocuments> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.GetDocflowAsync(AccountId, DocflowId, timeout);
        }

        public Task<IDocflowWithDocuments?> TryGetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.TryGetDocflowAsync(AccountId, DocflowId, timeout);
        }

        public Task<IDocflowWithDocuments> PatchAsync(JsonPatchDocument<IDocflowWithDocuments> patch, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.PatchDocflowAsync(AccountId, DocflowId, patch, timeout);
        }

        public Task<DocumentsRequest> GenerateDocumentsRequestAsync(CertificateContent certificate, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.GenerateDocumentsRequestAsync(AccountId, DocflowId, certificate.ToBytes(), timeout);
        }
    }
}