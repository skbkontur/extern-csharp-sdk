using System.Net.Http;

namespace ExternDotnetSDK.Clients.Common
{
    public interface IHttpClient
    {
        HttpClient Client { get; }
    }
}