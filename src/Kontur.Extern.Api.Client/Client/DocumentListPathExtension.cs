using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Docflows.Documents;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DocumentListPathExtension
    {
        public static Task<List<Document>> ListAsync(this DocumentListPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.GetDocumentsAsync(path.AccountId, path.DocflowId, timeout);
        }
    }
}