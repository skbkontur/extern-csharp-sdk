using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.Converters.EnumConverters;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Http.Exceptions
{
    internal static class Errors
    {
        public static Exception UrlShouldBeAbsolute([InvokerParameterName] string paramName, Uri uri) => 
            new ArgumentException($"Url must be an absolute URI. Instead, got this: '{uri}'.", paramName);
        
        public static Exception ValueShouldBeGreaterOrEqualTo([InvokerParameterName] string paramName, long actualValue, long minimumValue) => 
            new ArgumentOutOfRangeException(paramName, actualValue, $"The value should be greater or equal to {minimumValue}");
        
        public static Exception ValueShouldBeGreaterThanZero([InvokerParameterName] string paramName, long actualValue) => 
            ValueShouldBeGreaterThan(paramName, actualValue, 0);
        
        public static Exception ValueShouldBeGreaterThan([InvokerParameterName] string paramName, long actualValue, long nonInclusiveLowerBound) => 
            new ArgumentOutOfRangeException(paramName, actualValue, $"The value should be greater than {nonInclusiveLowerBound}");
        
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
        
        public static Exception JsonTokenIsUnexpected(JsonTokenType actualToken, params JsonTokenType[] expectedTokens) => 
            new JsonException($"Unexpected token {actualToken}. Expected {string.Join(", or", expectedTokens)}.");
        
        public static Exception EnumValueOfBackingTypeIsNotSupported(TypeCode typeCode) => 
            new NotSupportedException($"Enum with backing type {typeCode} is not supported.");
        
        public static Exception CannotReadNumberFromJsonValue() => 
            new JsonException("Cannot read number from json value");
        
        public static Exception UnknownJsonBooleanValue(string value, params string[] expectedValues) => 
            new JsonException($"Unexpected json property value \"{value}\". Possible values {string.Join(", or", expectedValues)}.");

        public static Exception JsonInvalidEnumValue(Type type, string? value) => 
            new JsonException($"Unexpected value of enum type {type}: {value ?? "<null>"}");

        public static Exception CannotDeserializeFromNullJson(Type type) => 
            new NotSupportedException($"An attempt to deserialize a null json into an object of type {type} failed: null JSON deserialization is not supported.");

        public static Exception DeserializationFailure(Exception exception) => 
            new JsonException("Deserialization failed", exception);

        public static Exception CannotParseJsonStringValueToEnumOfType(string? stringValue, Type type) =>
            new JsonException($"Cannot parse string value {stringValue} to enum of type {type}");

        public static Exception OverridingJsonStringEnumConverterIsUnsupported([InvokerParameterName] string paramName) =>
            new ArgumentException($"Overriding a {nameof(JsonStringEnumConverter)} is unsupported, use {nameof(NamingPolicyRespectJsonStringEnumConverter)} instead", paramName);

        public static Exception InvalidRange<T>([InvokerParameterName] string minParamName, [InvokerParameterName] string maxParamName, T min, T max) => 
            new ArgumentException($"Invalid range: the value {min} of the parameter {minParamName} is greater than the value {max} of the parameter {maxParamName}.");
    }
}