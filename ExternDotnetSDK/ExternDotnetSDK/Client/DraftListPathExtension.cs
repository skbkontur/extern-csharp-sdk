using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Model.Drafts;
using Kontur.Extern.Api.Client.Models.Drafts;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client
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