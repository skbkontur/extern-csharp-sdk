using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Paths;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class InventoryDocflowPathExtension
    {
        [ItemCanBeNull]
        public static Task<Docflow> TryGetAsync(this in InventoryDocflowPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.TryGetInventoryDocflowAsync(path.AccountId, path.DocflowId, path.DocumentId, path.InventoryId, timeout);
        }
        
        public static Task<Docflow> GetAsync(this in InventoryDocflowPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.GetInventoryDocflowAsync(path.AccountId, path.DocflowId, path.DocumentId, path.InventoryId, timeout);
        }
    }
}