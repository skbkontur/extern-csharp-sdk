using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Drafts;
using Kontur.Extern.Client.Model.Drafts;
using Kontur.Extern.Client.Paths;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class DraftListPathExtension
    {
        public static Task<Draft> CreateDraftAsync(this in DraftListPath path, DraftMetadata draftMetadata, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.CreateDraftAsync(path.AccountId, draftMetadata.ToRequest(), timeout);
        }
    }
}