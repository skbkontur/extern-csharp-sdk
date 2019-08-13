using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Docflows;
using Refit;

namespace ExternDotnetSDK.Clients.RelatedDocflows
{
    public class RelatedDocflowsClient : InnerCommonClient, IRelatedDocflowsClient
    {
        public RelatedDocflowsClient(ILog log, HttpClient client)
            : base(log) =>
            ClientRefit = RestService.For<IRelatedDocflowsClientRefit>(client);

        public IRelatedDocflowsClientRefit ClientRefit { get; }

        public async Task<DocflowPage> GetRelatedDocflows(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter) => await TryExecuteTask(
            ClientRefit.GetRelatedDocflows(accountId, relatedDocflowId, relatedDocumentId, filter));
    }
}