using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.HttpLevel
{
    public interface IHttpRequest
    {
        Task<IHttpResponse> SendAsync(TimeSpan? timeout = null);
    }
}