using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Models.Docflows;

namespace ExternDotnetSDK.Clients.RelatedDocflows
{
    public interface IRelatedDocflowsClient : IHttpClient
    {
        //todo this method was not tested at all
        Task<DocflowPage> GetRelatedDocflows(Guid accountId, Guid relatedDocflowId, Guid relatedDocumentId, DocflowFilter filter);
    }
}