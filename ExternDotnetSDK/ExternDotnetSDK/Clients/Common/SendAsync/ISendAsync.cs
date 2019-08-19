using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common.ResponseMessage;

namespace ExternDotnetSDK.Clients.Common.SendAsync
{
    public interface ISendAsync
    {
        Task<IHaveHttpResponseMessage> SendAsync(HttpRequestMessage request, params object[] extraParams);
    }
}