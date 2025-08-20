using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.DocumentsRequests;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct DocumentsRequestPath
    {
        public DocumentsRequestPath(Guid accountId, Guid docflowId, Guid requestId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            RequestId = requestId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }

        public Guid DocflowId { get; }

        public Guid RequestId { get; }

        public IExternClientServices Services { get; }

        public Task<DocumentsRequest> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.GetDocumentsRequestAsync(AccountId, DocflowId, RequestId, timeout);
        }

        public Task<IDocflowWithDocuments> SendAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.SendDocumentsRequestAsync(AccountId, DocflowId, RequestId, timeout);
        }

        public Task<DocumentsRequest> UpdateSignatureAsync(Signature signature, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Docflows.UpdateDocumentsRequestSignatureAsync(AccountId, DocflowId, RequestId, signature.ToBytes(), timeout);
        }
    }
}