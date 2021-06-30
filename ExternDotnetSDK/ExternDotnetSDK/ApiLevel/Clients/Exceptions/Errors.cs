using System;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.ApiLevel.Clients.Exceptions
{
    internal static class Errors
    {
        public static Exception ResponseHasToHaveBody(string request) => 
            new ContractException($"The response on the request {request} does not have any content.");

        public static Exception ResponseHasUnexpectedContentType(string request, Response response, string expectedContentType) => 
            new ContractException($"The response on the request {request} is expected to have content type '{expectedContentType}', but it have '{response.Headers.ContentType}'");
    }
}