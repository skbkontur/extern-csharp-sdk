using System;
using System.Collections.Generic;
using System.Text.Json;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ApiErrors;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.Common.Time;
using static System.Environment;

namespace Kontur.Extern.Api.Client.Exceptions
{
    internal static class Errors
    {
        public static ArgumentOutOfRangeException UnexpectedEnumMember<T>([InvokerParameterName] string paramName, T enumValue)
            where T : Enum
        {
            return new(paramName, enumValue, null);
        }

        public static Exception ValueShouldBeGreaterOrEqualTo([InvokerParameterName] string paramName, long actualValue, long minimumValue) => 
            new ArgumentOutOfRangeException(paramName, actualValue, $"The value should be greater or equal to {minimumValue}");
        
        public static Exception ValueShouldBeGreaterThanZero([InvokerParameterName] string paramName, long actualValue) => 
            ValueShouldBeGreaterThan(paramName, actualValue, 0);

        public static Exception ValueShouldBePositive([InvokerParameterName] string paramName, long actualValue) =>
            new ArgumentOutOfRangeException(paramName, actualValue, "The value cannot be less then zero");
        
        public static Exception ValueCannotBeLessThenAnother([InvokerParameterName] string paramName, [InvokerParameterName] string comparableParamName, long actualValue, long comparableValue) =>
            new ArgumentOutOfRangeException(paramName, actualValue, $"The value cannot be less then the value {comparableValue} of the parameter {comparableParamName}");
        
        public static Exception ValueCannotBeLessOrEqualThenAnother([InvokerParameterName] string paramName, [InvokerParameterName] string comparableParamName, long actualValue, long comparableValue) =>
            new ArgumentOutOfRangeException(paramName, actualValue, $"The value cannot be less or equal then the value {comparableValue} of the parameter {comparableParamName}");
        
        public static Exception ValueCannotBeGreaterOrEqualThenAnother([InvokerParameterName] string paramName, [InvokerParameterName] string comparableParamName, long actualValue, long comparableValue) =>
            new ArgumentOutOfRangeException(paramName, actualValue, $"The value cannot be greater or equal then the value {comparableValue} of the parameter {comparableParamName}");
        
        public static Exception ValueShouldBeGreaterThan([InvokerParameterName] string paramName, long actualValue, long nonInclusiveLowerBound) => 
            new ArgumentOutOfRangeException(paramName, actualValue, $"The value should be greater than {nonInclusiveLowerBound}");
        
        public static Exception ListCannotBeGreaterThanParamSpecify<T>([InvokerParameterName] string listParamName, [InvokerParameterName] string boundParamName, IReadOnlyList<T> list, long maxSize) => 
            new ArgumentException($"The list have {list.Count} items, but it exceeds maximum size {maxSize} specified by the '{boundParamName}' parameter", listParamName);

        public static Exception IntermediatePageSizeCannotBeLessThanPageSize([InvokerParameterName] string paramName, int itemsCount, long pageIndex, uint pageSize) => 
            new ArgumentException($"The {pageIndex} page is intermediate and cannot contain less items than page size {pageSize}, but it has {itemsCount}", paramName);
        
        public static Exception ItemsOfLastPageIsGreaterThanLeftItems([InvokerParameterName] string paramName, int itemsCount, long leftItems) => 
            new ArgumentException($"The give items count {itemsCount} is greater than left for the last page {leftItems}", paramName);

        public static ArgumentException InvalidKppAuthorityNumber([InvokerParameterName] string paramName, string value)
        {
            return new($"The given KPP '{value}' does not match the KPP format. The KPP code should have 9 digits, except 5th and 6th positions which are able to be digits or UPPER case latin letters (A..Z)", paramName);
        }

        public static ArgumentException InvalidOkpo([InvokerParameterName] string paramName, string value) => 
            new($"The given OKPO '{value}' does not match to one of OKPO formats. The OKPO should have 10 digits code (for individual entrepreneur) or 8 digits code (for legal entity)", paramName);

        public static ArgumentException InvalidAuthorityNumber([InvokerParameterName] string paramName, string value, AuthorityNumberKind numberKind, string format)
        {
            var formatName = GetFormatName(numberKind);
            return new($"The given value '{value}' does not match the {formatName} format. The value should match to {format}, where X is a digit from 0 to 9", paramName);
        }

        private static string GetFormatName(AuthorityNumberKind numberKind)
        {
            return numberKind switch
            {
                AuthorityNumberKind.FnsAuthorityCode => "Fns",
                AuthorityNumberKind.PfrAuthorityCode => "Pfr",
                AuthorityNumberKind.FssAuthorityCode => "Fss",
                AuthorityNumberKind.RosstatAuthorityCode => "Rosstat",
                AuthorityNumberKind.Knd => "KND",
                AuthorityNumberKind.Okpo => "OKPO",
                AuthorityNumberKind.Okud => "OKUD",
                AuthorityNumberKind.InnKpp => "INN-KPP",
                AuthorityNumberKind.Inn => "INN",
                AuthorityNumberKind.Kpp => "Kpp",
                AuthorityNumberKind.LegalEntityInn => "INN of legal entity",
                AuthorityNumberKind.PfrRegNumber => "PFR reg number",
                AuthorityNumberKind.FssRegNumber => "FSS reg number",
                AuthorityNumberKind.IfnsCode => "IFNS code",
                AuthorityNumberKind.MriCode => "MRI code",
                AuthorityNumberKind.FssCode => "FSS code",
                AuthorityNumberKind.Togs => "TOGS",
                AuthorityNumberKind.UpfrCode => "UPFR code",
                _ => throw UnexpectedEnumMember(nameof(numberKind), numberKind)
            };
        }
        
        public static Exception InvalidRange([InvokerParameterName] string fromParamName, [InvokerParameterName] string toParamName, DateOnly from, DateOnly to) => 
            new ArgumentException($"Invalid range bounds, the value '{@from}' of '{fromParamName}' parameter is greater than the value '{to}' of '{toParamName}' parameter");
        
        public static Exception InvalidRange([InvokerParameterName] string fromParamName, [InvokerParameterName] string toParamName, DateTime from, DateTime to) => 
            new ArgumentException($"Invalid range bounds, the value '{@from}' of '{fromParamName}' parameter is greater than the value '{to}' of '{toParamName}' parameter");

        public static ApiException LongOperationFailed(ApiError startApiError) => 
            new($"{startApiError}{NewLine}{startApiError.Message}");

        public static Exception StringShouldNotBeNullOrWhiteSpace([InvokerParameterName] string paramName) => 
            new ArgumentException("The given value cannot be null, or empty, or a whitespace string.", paramName);

        public static Exception ValueShouldNotBeEmpty([InvokerParameterName] string paramName) => 
            new ArgumentException("The given value cannot be empty.", paramName);

        public static Exception TheAuthProviderNotSpecifiedOrUnsupported() => 
            new InvalidOperationException("There is no specified an authentication provider or the specified one is not supported");

        public static Exception UnsuccessfulApiResponse(ApiError apiErrorResponse) =>
            new ApiException(apiErrorResponse.ToString());

        public static Exception UrlShouldBeAbsolute([InvokerParameterName] string paramName, Uri uri) => 
            new ArgumentException($"The value '{uri}' is not be absolute url", paramName);

        public static Exception ArrayCannotBeEmpty([InvokerParameterName] string paramName) =>
            new ArgumentException("Value cannot be an empty array.", paramName);

        public static Exception BytesArrayCannotContainsOnlyZeros(string paramName) => 
            new ArgumentException("The array cannot contains only zero bytes.", paramName);
        
        public static Exception StringsCannotContainNullOrWhitespace(string paramName) => 
            new ArgumentException("The collection cannot contains null, or empty, or whitespace strings.", paramName);

        public static Exception TheOffsetCannotExceedBufferLength([InvokerParameterName] string paramName, int offset, int bufferLength) => 
            new ArgumentOutOfRangeException(paramName, offset, $"The offset cannot exceed the buffer length, which is {bufferLength}");

        public static Exception TheCountCannotBeOutOfBuffer([InvokerParameterName] string paramName, int count, int bufferLength) => 
            new ArgumentOutOfRangeException(paramName, count, $"Count cannot be out of buffer range, which length is {bufferLength}");

        public static Exception TheResponseDoesNotHaveContentRangeHeader() => 
            new ApiException("The response does not have CONTENT-RANGE header");

        public static Exception ContentPartCannotHaveEmptyBytes([InvokerParameterName] string paramName) => 
            new ArgumentException("The content part cannot have empty bytes", paramName);

        public static Exception TheContentIsAlreadyEndedAndNextPartCannotBeLoaded() => 
            new ApiException("The content is already ended and there is no next part of it to download");

        public static Exception UrnDoesNotBelongToNamespace([InvokerParameterName] string paramName, Urn urn, Urn ns) => 
            new ArgumentOutOfRangeException(paramName, urn, $"The given URN does not belong to the namespace '{ns}'");

        public static Exception JsonDoesNotContainProperty(string propName) => 
            new JsonException($"The JSON does not contain property {propName}.");

        public static Exception UnknownSubtypeOf<T>(Type subType) => 
            new InvalidOperationException($"Unknown subtype {subType} of {typeof(T)}");

        public static Exception JsonPropertyCannotBeNullIfAnotherPropertyHasValue(string propertyName, string anotherPropertyName, string? anotherPropertyValue) => 
            new JsonException($"The json property '{propertyName}' cannot be null, if the another property '{anotherPropertyName}' has following value '{anotherPropertyValue ?? "null"}'");

        public static Exception CannotCreateApiTaskResultWithIsEmptyResult<TResult>([InvokerParameterName] string paramName, TResult result) =>
            new ArgumentException($"Cannot create api task result when given an empty parameter. The parameter value:{NewLine}{result}.", paramName);

        public static Exception InvalidSvdregCode([InvokerParameterName] string paramName, string value) => 
            new ArgumentException($"The given SVDREG code '{value}' is invalid. It should start with 0 or X symbol then continue with 4 digits.", paramName);

        public static Exception InvalidUrnSchema([InvokerParameterName] string paramName, string value) => 
            new ArgumentException($"Invalid URN schema. Value: {value}", paramName);

        public static Exception UrnCannotHaveTrailingWhitespaces([InvokerParameterName] string paramName, string value) => 
            new ArgumentException($"URN cannot have trailing schema. Value: {value}", paramName);
    }
}