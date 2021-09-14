using System;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters
{
    internal static class RequestExtension
    {
        public static bool IsWriteRequest(this Request request) => !request.Method.Equals(RequestMethods.Get, StringComparison.OrdinalIgnoreCase);
    }
}