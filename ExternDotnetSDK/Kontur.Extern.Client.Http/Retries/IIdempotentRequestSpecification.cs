using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Http.Retries
{
    public interface IIdempotentRequestSpecification
    {
        bool IsIdempotent(Request request);
    }
}