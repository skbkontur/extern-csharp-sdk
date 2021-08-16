using System.Net.Http;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Http
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string relativeUrl, T request);
    }
}