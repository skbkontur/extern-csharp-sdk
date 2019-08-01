using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Drafts;
using ExternDotnetSDK.Drafts.Requests;
using Refit;

namespace ExternDotnetSDK.Clients.Drafts
{
    internal interface IDraftClientRefit
    {
        [Post("/v1/{accountId}/drafts")]
        Task<Draft> CreateDraftAsync(Guid accountId, [Body] DraftMetaRequest draftRequest);
    }
}