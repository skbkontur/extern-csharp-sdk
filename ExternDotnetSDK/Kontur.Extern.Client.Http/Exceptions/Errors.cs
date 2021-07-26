using System;
using JetBrains.Annotations;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Http.Exceptions
{
    internal static class Errors
    {
        public static Exception ResponseHasToHaveBody(string request) => 
            new ContractException($"The response on the request {request} does not have any content.");

        public static Exception ResponseHasUnexpectedContentType(string request, Response response, string expectedContentType) => 
            new ContractException($"The response on the request {request} is expected to have content type '{expectedContentType}', but it have '{response.Headers.ContentType}'");

        public static Exception TimeSpanOutOfRange([InvokerParameterName] string paramName, TimeSpan actualValue, TimeSpan lowBound, TimeSpan highBound) =>
            new ArgumentOutOfRangeException(paramName, actualValue, $"The duration interval should be within range [{lowBound}, {highBound}]");

        public static Exception UnsuccessfulResponse(ResponseCode statusCode) => new ContractException($"Response status code '{statusCode}' indicates unsuccessful outcome.");

        public static Exception StringShouldNotBeNullOrWhiteSpace([InvokerParameterName] string paramName) => 
            new ArgumentException("The given value cannot be null, or empty, or a whitespace string.", paramName);
    }
}