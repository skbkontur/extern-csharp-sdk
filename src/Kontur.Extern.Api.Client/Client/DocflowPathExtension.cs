using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.DocumentsRequests;
using Kontur.Extern.Api.Client.Paths;
using Microsoft.AspNetCore.JsonPatch;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DocflowPathExtension
    {
        public static Task<IDocflowWithDocuments> GetAsync(this in DocflowPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.GetDocflowAsync(path.AccountId, path.DocflowId, timeout);
        }

        public static Task<IDocflowWithDocuments?> TryGetAsync(this in DocflowPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.TryGetDocflowAsync(path.AccountId, path.DocflowId, timeout);
        }

        public static Task<IDocflowWithDocuments> PatchAsync(this in DocflowPath path, JsonPatchDocument<IDocflowWithDocuments> patch, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.PatchDocflowAsync(path.AccountId, path.DocflowId, patch, timeout);
        }

        public static Task<DocumentsRequest> GenerateDocumentsRequestAsync(this in DocflowPath path, CertificateContent certificate, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.GenerateDocumentsRequestAsync(path.AccountId, path.DocflowId, certificate.ToBytes(), timeout);
        }
    }
}