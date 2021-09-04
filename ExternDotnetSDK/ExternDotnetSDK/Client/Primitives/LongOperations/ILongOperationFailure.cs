using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Primitives.LongOperations
{
    public interface ILongOperationFailure
    {
        ApiException ToException();
    }
}