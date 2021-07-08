using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Paths;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class InventoryPathExtension
    {
        public static Task<Docflow> GetAsync(this in InventoryPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.GetInventoryDocflowAsync(path.AccountId, path.DocflowId, path.DocumentId, path.InventoryId, timeout);
        }
    }
}