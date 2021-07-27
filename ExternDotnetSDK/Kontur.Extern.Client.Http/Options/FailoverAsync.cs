using System.Threading.Tasks;

namespace Kontur.Extern.Client.Http.Options
{
    public delegate Task<FailoverDecision> FailoverAsync(IHttpResponse response, uint attempt);
}