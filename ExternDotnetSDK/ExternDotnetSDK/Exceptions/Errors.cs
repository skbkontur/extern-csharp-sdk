using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using Kontur.Extern.Client.Model.Numbers;
using Vostok.Clusterclient.Core.Model;
using static System.Environment;

namespace Kontur.Extern.Client.Exceptions
{
    internal static class Errors
    {
        public static ArgumentOutOfRangeException UnexpectedEnumMember<T>([InvokerParameterName] string paramName, T enumValue)
            where T : Enum
        {
            return new(paramName, enumValue, null);
        }

        public static Exception ResponseHasToHaveBody(string request) => 
            new ContractException($"The response on the request {request} does not have any content.");

        public static Exception ResponseHasUnexpectedContentType(string request, Response response, string expectedContentType) => 
            new ContractException($"The response on the request {request} is expected to have content type '{expectedContentType}', but it have '{response.Headers.ContentType}'");

        public static Exception ValueShouldBeGreaterOrEqualTo([InvokerParameterName] string paramName, uint actualValue, int minimumValue) => 
            new ArgumentOutOfRangeException(paramName, actualValue, $"The value should be greater or equal to {minimumValue}");
        
        public static Exception ValueShouldBeGreaterThanZero([InvokerParameterName] string paramName, uint actualValue) => 
            ValueShouldBeGreaterThan(paramName, actualValue, 0);
        
        public static Exception ValueShouldBeGreaterThan([InvokerParameterName] string paramName, long actualValue, long nonInclusiveLowerBound) => 
            new ArgumentOutOfRangeException(paramName, actualValue, $"The value should be greater than {nonInclusiveLowerBound}");
        
        public static Exception TimeSpanShouldBePositive([InvokerParameterName] string paramName, TimeSpan actualValue) => 
            new ArgumentOutOfRangeException(paramName, actualValue, $"The duration interval should be positive");

        public static Exception ListCannotBeGreaterThanParamSpecify<T>([InvokerParameterName] string listParamName, [InvokerParameterName] string boundParamName, IReadOnlyList<T> list, long maxSize) => 
            new ArgumentException($"The list have {list.Count} items, but it exceeds maximum size {maxSize} specified by the '{boundParamName}' parameter", listParamName);

        public static Exception IntermediatePageSizeCannotBeLessThanPageSize([InvokerParameterName] string paramName, int itemsCount, long pageIndex, uint pageSize) => 
            new ArgumentException($"The {pageIndex} page is intermediate and cannot contain less items than page size {pageSize}, but it has {itemsCount}", paramName);
        
        public static Exception ItemsOfLastPageIsGreaterThanLeftItems([InvokerParameterName] string paramName, int itemsCount, long leftItems) => 
            new ArgumentException($"The give items count {itemsCount} is greater than left for the last page {leftItems}", paramName);

        public static Exception InvalidRosstatSupervisoryAuthorityNumber([InvokerParameterName] string paramName, string value) => 
            InvalidSupervisoryAuthorityNumber(paramName, value, "Rosstat", "XX-XX");
        
        public static Exception InvalidFssSupervisoryAuthorityNumber([InvokerParameterName] string paramName, string value) => 
            // ReSharper disable once StringLiteralTypo
            InvalidSupervisoryAuthorityNumber(paramName, value, "FSS", "XXXXX");
        
        public static Exception InvalidFnsSupervisoryAuthorityNumber([InvokerParameterName] string paramName, string value) => 
            // ReSharper disable once StringLiteralTypo
            InvalidSupervisoryAuthorityNumber(paramName, value, "FNS", "ХХХХ");
        
        public static Exception InvalidPfrSupervisoryAuthorityNumber([InvokerParameterName] string paramName, string value) => 
            // ReSharper disable once StringLiteralTypo
            InvalidSupervisoryAuthorityNumber(paramName, value, "PFR", "ХХХ-ХХХ");

        public static ArgumentException InvalidSupervisoryAuthorityNumber([InvokerParameterName] string paramName, string value, string formatName, string formatDetails) => 
            new($"The given value '{value}' does not match the {formatName} format. The value should match to {formatDetails}, where X is a digit from 0 to 9", paramName);

        public static ArgumentException InvalidAuthorityNumber([InvokerParameterName] string paramName, string value, AuthorityNumberKind numberKind, string format)
        {
            var formatName = GetFormatName(numberKind);
            return new($"The given value '{value}' does not match the {formatName} format. The value should match to {format}, where X is a digit from 0 to 9", paramName);
        }

        private static string GetFormatName(AuthorityNumberKind numberKind)
        {
            return numberKind switch
            {
                AuthorityNumberKind.SupervisoryAuthorityFns => "Fns",
                AuthorityNumberKind.SupervisoryAuthorityPfr => "Pfr",
                AuthorityNumberKind.SupervisoryAuthorityFss => "Fss",
                AuthorityNumberKind.SupervisoryAuthorityRosstat => "Rosstat",
                AuthorityNumberKind.Knd => "KND",
                AuthorityNumberKind.Okpo => "OKPO",
                AuthorityNumberKind.Okud => "OKUD",
                AuthorityNumberKind.InnKpp => "INN-KPP",
                AuthorityNumberKind.Inn => "INN",
                AuthorityNumberKind.PfrRegNumber => "PFR reg number",
                _ => throw UnexpectedEnumMember(nameof(numberKind), numberKind)
            };
        }

        public static Exception InvalidRange([InvokerParameterName] string fromParamName, [InvokerParameterName] string toParamName, DateTime from, DateTime to) => 
            new ArgumentException($"Invalid range bounds, the value '{@from}' of '{fromParamName}' parameter is greater than the value '{to}' of '{toParamName}' parameter");

        public static Exception LongOperationFailed(Error startError) => new ApiException($"{startError}{NewLine}{startError.Message}");

        public static Exception UnsuccessfulResponse(ResponseCode statusCode) => new ApiException($"Response status code '{statusCode}' indicates unsuccessful outcome.");

        public static Exception StopwatchHaveToBeRunning([InvokerParameterName] string paramName) => new ArgumentException("The stopwatch have to be running", paramName);
        
        public static Exception TimeIntervalShouldBeNonEmpty([InvokerParameterName] string paramName) => 
            new ArgumentException("The duration interval should not be empty", paramName);

        public static Exception StringShouldNotBeEmptyOrWhiteSpace([InvokerParameterName] string paramName) => 
            new ArgumentException("The given value cannot be null, or empty, or a whitespace string.", paramName);

        public static Exception AccessTokenAlreadyExpired([InvokerParameterName] string paramName) => 
            new ArgumentException("The access token has been expired already", paramName);

        public static Exception ArrayCannotBeEmpty([InvokerParameterName] string paramName) =>
            new ArgumentException("Value cannot be an empty collection.", paramName);
    }
}