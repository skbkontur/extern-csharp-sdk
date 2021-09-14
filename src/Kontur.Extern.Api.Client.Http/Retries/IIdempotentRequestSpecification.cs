using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http.Retries
{
    public interface IIdempotentRequestSpecification
    {
        bool IsIdempotent(Request request);
    }
}