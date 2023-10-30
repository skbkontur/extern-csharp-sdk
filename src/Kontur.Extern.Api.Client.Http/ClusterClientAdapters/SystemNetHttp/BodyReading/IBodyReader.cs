using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.BodyReading
{
    internal interface IBodyReader
    {
        Task<BodyReadResult> ReadAsync(HttpResponseMessage message, CancellationToken cancellationToken);
    }
}