using System.Threading.Tasks;
using Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Http;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool
{
    internal interface IExternTestToolCommand<T>
    {
        Task<T> ExecuteAsync(IHttpClient httpClient, IResponseCache cache);
    }
}