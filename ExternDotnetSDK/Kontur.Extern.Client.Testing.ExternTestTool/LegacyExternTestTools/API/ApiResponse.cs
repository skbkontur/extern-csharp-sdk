using System.Collections.Generic;
using System.Net;

namespace Kontur.Extern.Client.Testing.ExternTestTool.LegacyExternTestTools.API
{
    public class ApiResponse<T>
    {
        public ApiResponse(int statusCode, IDictionary<string, string> headers, T data)
        {
            StatusCode = (HttpStatusCode) statusCode;
            Headers = headers;
            Data = data;
        }

        public HttpStatusCode StatusCode { get; }
        public IDictionary<string, string> Headers { get; }
        public T Data { get; }
    }
}