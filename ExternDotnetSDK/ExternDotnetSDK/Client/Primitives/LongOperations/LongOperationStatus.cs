#nullable enable
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Clients.Exceptions;
using Kontur.Extern.Client.ApiLevel.Models.Errors;

namespace Kontur.Extern.Client.Primitives.LongOperations
{
    [PublicAPI]
    public class LongOperationStatus<T>
        where T : class
    {
        public static readonly LongOperationStatus<T> InProgress = new(default, null); 
        
        public static LongOperationStatus<T> Completed(T result) => new(result, null);
        
        public static LongOperationStatus<T> Failed(Error error) => new(default, error); 
        
        private readonly T? result;
        private readonly Error? error;

        private LongOperationStatus(T? result, Error? error)
        {
            this.result = result;
            this.error = error;
        }

        public bool IsFailed => error != null;
        public bool IsCompleted => ReferenceEquals(result, null);

        public bool TryGetResult(out T completionResult)
        {
            if (result != null)
            {
                completionResult = result;
                return true;
            }

            completionResult = default!;
            return false;
        }

        public bool TryGetError(out Error failureError)
        {
            if (error != null)
            {
                failureError = error;
                return true;
            }

            failureError = default!;
            return false;
        }

        public LongOperationStatus<T> EnsureSuccess()
        {
            if (error != null)
                throw Errors.LongOperationFailed(error);
            return this;
        }
    }
    
    [PublicAPI]
    public class LongOperationStatus
    {
        public static readonly LongOperationStatus InProgress = new(false, null); 
        
        public static readonly LongOperationStatus Completed = new(true, null);
        
        public static LongOperationStatus Failed(Error error) => new(false, error);

        private readonly Error? error;

        private LongOperationStatus(bool completed, Error? error)
        {
            IsCompleted = completed;
            this.error = error;
        }

        public bool IsFailed => error != null;
        public bool IsCompleted { get; }

        public bool TryGetError(out Error failureError)
        {
            if (error != null)
            {
                failureError = error;
                return true;
            }

            failureError = default!;
            return false;
        }

        public LongOperationStatus EnsureSuccess()
        {
            if (error != null)
                throw Errors.LongOperationFailed(error);
            return this;
        }
    }
}