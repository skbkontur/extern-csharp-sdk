using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Docflows;

namespace ExternDotnetSDK.Clients.RelatedDocflows
{
    public class RelatedDocflowsClient : InnerCommonClient, IRelatedDocflowsClient
    {
        public RelatedDocflowsClient(ILogError logError, HttpClient client)
            : base(logError, client)
        {
        }

        public async Task<DocflowPage> GetRelatedDocflows(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter) =>
            await SendRequestAsync<DocflowPage>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/related",
                filter.ConvertToQueryParameters());
    }
}