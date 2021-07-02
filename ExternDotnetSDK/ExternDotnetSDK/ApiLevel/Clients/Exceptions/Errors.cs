using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.ApiLevel.Clients.Exceptions
{
    internal static class Errors
    {
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

        public static Exception ListCannotBeGreaterThanParamSpecify<T>([InvokerParameterName] string listParamName, [InvokerParameterName] string boundParamName, IReadOnlyList<T> list, long maxSize) => 
            new ArgumentException($"The list have {list.Count} items, but it exceeds maximum size {maxSize} specified by the '{boundParamName}' parameter", listParamName);

        public static Exception IntermediatePageSizeCannotBeLessThanPageSize([InvokerParameterName] string paramName, int itemsCount, long pageIndex, uint pageSize) => 
            new ArgumentException($"The {pageIndex} page is intermediate and cannot contain less items than page size {pageSize}, but it has {itemsCount}", paramName);
        
        public static Exception ItemsOfLastPageIsGreaterThanLeftItems([InvokerParameterName] string paramName, int itemsCount, long leftItems) => 
            new ArgumentException($"The give items count {itemsCount} is greater than left for the last page {leftItems}", paramName);
    }
}