using Kontur.Extern.Api.Client.Exceptions;

namespace Kontur.Extern.Api.Client.Primitives.LongOperations
{
    public interface ILongOperationFailure
    {
        ApiException ToException();
    }
}