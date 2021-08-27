using System;
using System.Text.Json;
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

        public static Exception ContentMustBeSpecifiedBeforeSetRangeHeader() => 
            new InvalidOperationException("A content must be specified in the request before setting the range header");

        public static Exception TotalLengthMustBeGreaterOrEqualToContentLength([InvokerParameterName] string paramName, long totalLength, long contentLength) => 
            new ArgumentOutOfRangeException(paramName, totalLength, $"The total length must be greater or equal to content-length value, which is {contentLength}");

        public static Exception ContentRangeMustHaveValidBounds([InvokerParameterName] string paramName, long from, long to) => 
            new ArgumentException($"The specified content-range have wrong bounds: it starts from {@from} and ends on {to}", paramName);
        
        public static Exception ContentRangeMustHaveEqualBytesAsContentLength([InvokerParameterName] string paramName, long from, long to, long contentLength) => 
            new ArgumentException($"The specified content-range [{from}, {to}] have contains different bytes as the CONTENT-LENGTH {contentLength}", paramName);
        
        public static Exception JsonIsNotAnObject() => 
            new JsonException("Json is not an object");

        public static Exception JsonTokenIsUnexpected(Type type, JsonTokenType actualToken, params JsonTokenType[] expectedTokens) => 
            new JsonException($"Unexpected token while deserialize {type}. Expected {string.Join(", or", expectedTokens)}, but the json has {actualToken}.");

        public static Exception JsonInvalidEnumValue(Type type, string? value) => 
            new JsonException($"Unexpected value of enum type {type}: {value ?? "<null>"}");
    }
}