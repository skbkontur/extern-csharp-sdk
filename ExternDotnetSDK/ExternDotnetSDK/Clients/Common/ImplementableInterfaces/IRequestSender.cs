using System.Threading.Tasks;

namespace ExternDotnetSDK.Clients.Common.ImplementableInterfaces
{
    /// <summary>
    ///     Implements a client that sends requests to server, like HttpClient class.
    /// </summary>
    public interface IRequestSender
    {
        Task<IHaveHttpResponseMessage> SendAsync(IHaveHttpRequestMessage request);
    }
}