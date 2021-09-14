using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Common.Exceptions
{
    internal static class Errors
    {
        public static Exception TimeSpanShouldBePositive([InvokerParameterName] string paramName, TimeSpan actualValue) => 
            new ArgumentOutOfRangeException(paramName, actualValue, "The duration interval should be positive");

        public static Exception StopwatchHaveToBeRunning([InvokerParameterName] string paramName) => 
            new ArgumentException("The stopwatch have to be running", paramName);
        
        public static Exception TimeIntervalShouldBeNonEmpty([InvokerParameterName] string paramName) => 
            new ArgumentException("The duration interval should not be empty", paramName);
    }
}