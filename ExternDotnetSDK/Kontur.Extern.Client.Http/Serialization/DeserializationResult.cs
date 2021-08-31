using System;
using Kontur.Extern.Client.Http.Exceptions;

namespace Kontur.Extern.Client.Http.Serialization
{
    public readonly struct DeserializationResult<T>
    {
        public static DeserializationResult<T> Failed(Exception exception) => 
            new(default, exception ?? throw new ArgumentNullException(nameof(exception)));

        public static DeserializationResult<T> Success(T? result) =>
            new(result, null);
        
        private readonly T? result;
        private readonly Exception? exception;

        private DeserializationResult(T? result, Exception? exception)
        {
            this.result = result;
            this.exception = exception;
        }
        
        public T EnsureSuccessfulNotNullResult() => 
            exception is not null 
                ? throw Errors.DeserializationFailure(exception)
                : result ?? throw Errors.CannotDeserializeFromNullJson(typeof (T));

        public DeserializationResult<T> EnsureSuccess() => 
            exception is not null ? throw Errors.DeserializationFailure(exception) : this;

        public T? GetResultOrNull() => 
            exception is not null ? default : result;
    }
}