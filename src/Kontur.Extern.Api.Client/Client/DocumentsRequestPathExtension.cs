using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.DocumentsRequests;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DocumentsRequestPathExtension
    {
        public static Task<DocumentsRequest> GetAsync(this in DocumentsRequestPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.GetDocumentsRequestAsync(path.AccountId, path.DocflowId, path.RequestId, timeout);
        }

        public static Task<IDocflowWithDocuments> SendAsync(this in DocumentsRequestPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.SendDocumentsRequestAsync(path.AccountId, path.DocflowId, path.RequestId, timeout);
        }

        public static Task<DocumentsRequest> UpdateSignatureAsync(this in DocumentsRequestPath path, Signature signature, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.UpdateDocumentsRequestSignatureAsync(path.AccountId, path.DocflowId, path.RequestId, signature.ToBytes(), timeout);
        }
    }
}