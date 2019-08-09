using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Docflows;

namespace ExternDotnetSDK.Clients.RelatedDocflows
{
    public interface IRelatedDocflowsClient
    {
        IRelatedDocflowsClientRefit ClientRefit { get; }

        Task<DocflowPage> GetRelatedDocflows(Guid accountId, Guid relatedDocflowId, Guid relatedDocumentId, DocflowFilter filter);
    }
}