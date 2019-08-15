//using System;
//using System.Threading.Tasks;
//using ExternDotnetSDK.Models.Api;
//using ExternDotnetSDK.Models.Common;
//using ExternDotnetSDK.Models.Drafts;
//using ExternDotnetSDK.Models.Drafts.Meta;
//using ExternDotnetSDK.Models.Drafts.Requests;
//using Refit;

//namespace ExternDotnetSDK.Clients.Drafts
//{
//    public interface IDraftClientRefit
//    {
//        //todo make test when this method works properly
//        [Post("/v1/{accountId}/drafts/{draftId}/cloud-sign")]
//        Task<SignInitResult> CloudSignDraftAsync(Guid accountId, Guid draftId);

//        //todo make tests
//        [Post("/v1/{accountId}/drafts/{draftId}/cloud-sign-confirm")]
//        Task<SignResult> CloudSignConfirmDraftAsync(Guid accountId, Guid draftId, Guid requestId, string code);
//    }
//}