using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Docflows;
using Refit;

namespace ExternDotnetSDK.Clients.RelatedDocflows
{
    public class RelatedDocflowsClient : IRelatedDocflowsClient
    {
        public IRelatedDocflowsClientRefit ClientRefit { get; }

        public RelatedDocflowsClient(HttpClient client) => ClientRefit = RestService.For<IRelatedDocflowsClientRefit>(client);

        public async Task<DocflowPage> GetRelatedDocflows(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter) =>
            await ClientRefit.GetRelatedDocflows(accountId, relatedDocflowId, relatedDocumentId, filter);
    }
}