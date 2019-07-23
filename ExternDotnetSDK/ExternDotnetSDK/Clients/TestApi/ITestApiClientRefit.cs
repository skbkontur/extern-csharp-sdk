using System;
using System.Threading.Tasks;
using Refit;

namespace ExternDotnetSDK.Clients.TestApi
{
    internal interface ITestApiClientRefit
    {
        [Get("/v1/poke/{accountId}/{docflowId}?times={times}")]
        Task Poke(Guid accountId, Guid docflowId, uint times = 1);
    }
}