using System.Net.Http;

namespace ExternDotnetSDK.Clients.Common.ResponseMessage
{
    public interface IHaveHttpResponseMessage
    {
        HttpResponseMessage HttpResponseMessage { get; }
    }
}