#nullable enable
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Drafts;
using Kontur.Extern.Client.Paths;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class DraftPathExtension
    {
        public static Task<Draft> GetAsync(this in DraftPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.GetDraftAsync(path.AccountId, path.DraftId, timeout);
        }
        
        public static Task<Draft?> TryGetAsync(this in DraftPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.TryGetDraftAsync(path.AccountId, path.DraftId, timeout);
        }
        
        public static Task DeleteAsync(this in DraftPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.DeleteDraftAsync(path.AccountId, path.DraftId, timeout);
        }
    }
}