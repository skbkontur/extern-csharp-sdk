using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows
{
    internal static class DocflowFilterExtension
    {
        public static void AppendToQuery(this DocflowFilter filter, RequestUrlBuilder urlBuilder)
        {
            foreach (var (name, value) in filter.ToQueryParameters())
            {
                urlBuilder.AppendToQuery(name, value);
            }
        }
    }
}