using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Http.Options
{
    public delegate Task<FailoverDecision> FailoverAsync(IHttpResponse response, uint attempt);
}