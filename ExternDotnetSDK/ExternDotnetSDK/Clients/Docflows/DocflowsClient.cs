using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Docflows;
using Refit;

namespace ExternDotnetSDK.Clients.Docflows
{
    public class DocflowsClient
    {
        private readonly IDocflowsClientRefit clientRefit;

        public DocflowsClient(HttpClient client) => 
            clientRefit = RestService.For<IDocflowsClientRefit>(client);

        public async Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter filter = null) =>
            await clientRefit.GetDocflows(accountId, filter ?? new DocflowFilter());
    }
}