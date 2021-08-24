#nullable enable
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Primitives.LongOperations
{
    [PublicAPI]
    public class LongOperationStatus<T>
    {
        public static readonly LongOperationStatus<T> InProgress = new(default, null, false); 
        
        public static LongOperationStatus<T> Completed(T result) => new(result, null, true);
        
        public static LongOperationStatus<T> Failed(ApiError apiError) => new(default, apiError, false);

        private readonly T? result;
        private readonly ApiError? error;

        private LongOperationStatus(T? result, ApiError? error, bool isCompleted)
        {
            this.result = result;
            this.error = error;
        }

        public bool IsFailed => error != null;
        public bool IsCompleted { get; }

        public bool TryGetResult(out T completionResult)
        {
            if (IsCompleted)
            {
                completionResult = result!;
                return true;
            }

            completionResult = default!;
            return false;
        }

        public bool TryGetError(out ApiError failureApiError)
        {
            if (error != null)
            {
                failureApiError = error;
                return true;
            }

            failureApiError = default!;
            return false;
        }

        public LongOperationStatus<T> EnsureSuccess()
        {
            if (error is not null)
                throw Errors.LongOperationFailed(error);
            return this;
        }
    }
    
    [PublicAPI]
    public class LongOperationStatus
    {
        public static readonly LongOperationStatus InProgress = new(false, null); 
        
        public static readonly LongOperationStatus Completed = new(true, null);
        
        public static LongOperationStatus Failed(ApiError apiError) => new(false, apiError);

        private readonly ApiError? error;

        private LongOperationStatus(bool completed, ApiError? error)
        {
            IsCompleted = completed;
            this.error = error;
        }

        public bool IsFailed => error != null;
        public bool IsCompleted { get; }

        public bool TryGetError(out ApiError failureApiError)
        {
            if (error != null)
            {
                failureApiError = error;
                return true;
            }

            failureApiError = default!;
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