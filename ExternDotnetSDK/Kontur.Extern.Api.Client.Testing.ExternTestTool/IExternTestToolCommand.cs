using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Testing.ExternTestTool.Http;
using Kontur.Extern.Api.Client.Testing.ExternTestTool.ResponseCaching;

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool
{
    internal interface IExternTestToolCommand<T>
    {
        Task<T> ExecuteAsync(IHttpClient httpClient, IResponseCache cache);
    }
}