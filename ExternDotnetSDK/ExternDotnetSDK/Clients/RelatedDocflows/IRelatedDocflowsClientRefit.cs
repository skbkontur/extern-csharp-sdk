using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Docflows;
using Refit;

namespace ExternDotnetSDK.Clients.RelatedDocflows
{
    public interface IRelatedDocflowsClientRefit
    {
        //todo this method was not tested at all
        [Get("/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/related")]
        Task<DocflowPage> GetRelatedDocflows(Guid accountId, Guid relatedDocflowId, Guid relatedDocumentId, DocflowFilter filter);
    }
}