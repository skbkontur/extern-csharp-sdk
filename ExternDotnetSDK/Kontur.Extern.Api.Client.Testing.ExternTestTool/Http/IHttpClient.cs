using System.Net.Http;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.Http
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string relativeUrl, T request);
    }
}