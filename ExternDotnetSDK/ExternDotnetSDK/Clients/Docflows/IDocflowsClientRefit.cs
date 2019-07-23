using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Docflows;
using Refit;

namespace ExternDotnetSDK.Clients.Docflows
{
    internal interface IDocflowsClientRefit
    {
        [Get("/v1/{accountId}/docflows")]
        Task<DocflowPage> GetDocflows(Guid accountId, DocflowFilter filter);
    }
}