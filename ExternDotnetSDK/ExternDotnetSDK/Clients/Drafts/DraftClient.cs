using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Drafts;
using ExternDotnetSDK.Drafts.Requests;
using Refit;

namespace ExternDotnetSDK.Clients.Drafts
{
    public class DraftClient
    {
        private readonly IDraftClientRefit clientRefit;

        public DraftClient(HttpClient client)
        {
            clientRefit = RestService.For<IDraftClientRefit>(client);
        }

        public async Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest draftRequest)
        {
            return await clientRefit.CreateDraftAsync(accountId, draftRequest);
        }
    }
}