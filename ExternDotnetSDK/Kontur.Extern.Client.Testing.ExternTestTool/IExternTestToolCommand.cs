using System.Threading.Tasks;
using Kontur.Extern.Client.Testing.ExternTestTool.Http;
using Kontur.Extern.Client.Testing.ExternTestTool.ResponseCaching;

namespace Kontur.Extern.Client.Testing.ExternTestTool
{
    internal interface IExternTestToolCommand<T>
    {
        Task<T> ExecuteAsync(IHttpClient httpClient, IResponseCache cache);
    }
}