using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Paths;

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
    }
}