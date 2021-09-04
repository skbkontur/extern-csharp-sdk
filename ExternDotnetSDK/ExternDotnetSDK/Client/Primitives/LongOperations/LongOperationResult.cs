#nullable enable
using System;

namespace Kontur.Extern.Client.Primitives.LongOperations
{
    public readonly struct LongOperationResult<TResult, TFailure>
        where TFailure : ILongOperationFailure
    {
        public static LongOperationResult<TResult, TFailure> Success(TResult result)
        {
            return new(
                result ?? throw new ArgumentNullException(nameof(result)),
                default
            );
        }
        
        public static LongOperationResult<TResult, TFailure> Failure(TFailure result)
        {
            return new(
                default,
                result ?? throw new ArgumentNullException(nameof(result))
            );
        }
        
        private readonly TResult? success;
        private readonly TFailure? failure;

        private LongOperationResult(TResult? success, TFailure? failure)
        {
            this.success = success;
            this.failure = failure;
        }
        
        public bool TryGetSuccessResult(out TResult result)
        {
            if (success is not null)
            {
                result = success!;
                return true;
            }

            result = default!;
            return false;
        }
        
        public bool TryGetFailureResult(out TFailure result)
        {
            if (failure is not null)
            {
                result = failure;
                return true;
            }

            result = default!;
            return false;
        }

        public TResult GetSuccessResult()
        {
            if (failure is not null)
                throw failure.ToException();

            return success!;
        }
    }
}