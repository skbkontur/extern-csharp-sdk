using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Documents;
using Kontur.Extern.Client.Paths;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class DocumentListPathExtension
    {
        public static Task<List<Document>> List(this DocumentListPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.GetDocumentsAsync(path.AccountId, path.DocflowId, timeout);
        }
    }
}