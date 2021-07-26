using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using Kontur.Extern.Client.Model.Numbers;
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

        public static Exception ValueShouldBeGreaterOrEqualTo([InvokerParameterName] string paramName, uint actualValue, int minimumValue) => 
            new ArgumentOutOfRangeException(paramName, actualValue, $"The value should be greater or equal to {minimumValue}");
        
        public static Exception ValueShouldBeGreaterThanZero([InvokerParameterName] string paramName, uint actualValue) => 
            ValueShouldBeGreaterThan(paramName, actualValue, 0);
        
        public static Exception ValueShouldBeGreaterThan([InvokerParameterName] string paramName, long actualValue, long nonInclusiveLowerBound) => 
            new ArgumentOutOfRangeException(paramName, actualValue, $"The value should be greater than {nonInclusiveLowerBound}");
        
        public static Exception TimeSpanShouldBePositive([InvokerParameterName] string paramName, TimeSpan actualValue) => 
            new ArgumentOutOfRangeException(paramName, actualValue, "The duration interval should be positive");

        public static Exception ListCannotBeGreaterThanParamSpecify<T>([InvokerParameterName] string listParamName, [InvokerParameterName] string boundParamName, IReadOnlyList<T> list, long maxSize) => 
            new ArgumentException($"The list have {list.Count} items, but it exceeds maximum size {maxSize} specified by the '{boundParamName}' parameter", listParamName);

        public static Exception IntermediatePageSizeCannotBeLessThanPageSize([InvokerParameterName] string paramName, int itemsCount, long pageIndex, uint pageSize) => 
            new ArgumentException($"The {pageIndex} page is intermediate and cannot contain less items than page size {pageSize}, but it has {itemsCount}", paramName);
        
        public static Exception ItemsOfLastPageIsGreaterThanLeftItems([InvokerParameterName] string paramName, int itemsCount, long leftItems) => 
            new ArgumentException($"The give items count {itemsCount} is greater than left for the last page {leftItems}", paramName);

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
                _ => throw UnexpectedEnumMember(nameof(numberKind), numberKind)
            };
        }

        public static Exception InvalidRange([InvokerParameterName] string fromParamName, [InvokerParameterName] string toParamName, DateTime from, DateTime to) => 
            new ArgumentException($"Invalid range bounds, the value '{@from}' of '{fromParamName}' parameter is greater than the value '{to}' of '{toParamName}' parameter");

        public static Exception LongOperationFailed(Error startError) => new ApiException($"{startError}{NewLine}{startError.Message}");

        public static Exception StopwatchHaveToBeRunning([InvokerParameterName] string paramName) => new ArgumentException("The stopwatch have to be running", paramName);
        
        public static Exception TimeIntervalShouldBeNonEmpty([InvokerParameterName] string paramName) => 
            new ArgumentException("The duration interval should not be empty", paramName);

        public static Exception StringShouldNotBeNullOrWhiteSpace([InvokerParameterName] string paramName) => 
            new ArgumentException("The given value cannot be null, or empty, or a whitespace string.", paramName);
        
        public static Exception StringShouldNotBeEmptyOrWhiteSpace([InvokerParameterName] string paramName) => 
            new ArgumentException("The given value cannot an empty or a whitespace string.", paramName);

        public static Exception AccessTokenAlreadyExpired([InvokerParameterName] string paramName) => 
            new ArgumentException("The access token has been expired already", paramName);

        public static Exception ArrayCannotBeEmpty([InvokerParameterName] string paramName) =>
            new ArgumentException("Value cannot be an empty collection.", paramName);

        public static Exception TheAuthProviderNotSpecifiedOrUnsupported() => 
            new InvalidOperationException("There is no specified an authentication provider or the specified one is not suppoted");

        public static Exception UnsuccessfulApiResponse(Error errorResponse) =>
            new ApiException($"[id: \"{errorResponse.Id}\", status: {errorResponse.StatusCode}, track-id: \"{errorResponse.TrackId}\"]{NewLine}" +
                             errorResponse.Message);

        public static Exception UrlShouldBeAbsolute([InvokerParameterName] string paramName, Uri uri) => 
            new ArgumentException($"The value '{uri}' is not be absolute url", paramName);
    }
}