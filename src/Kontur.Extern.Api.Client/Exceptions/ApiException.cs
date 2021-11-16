using System;
using System.Net;
using Kontur.Extern.Api.Client.Models.ApiErrors;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
        public ApiException(string message)
            : base(message)
        {
            Id = new Urn("urn:error", "unknown");
        }

        public ApiException(Urn id, HttpStatusCode statusCode, string message)
            : base(message)
        {
            Id = id;
            StatusCode = statusCode;
        }

        public ApiException(Urn id, HttpStatusCode statusCode, string message, Exception inner)
            : base(message, inner)
        {
            Id = id;
            StatusCode = statusCode;
        }

        public ApiException(ApiError apiError)
            : base(apiError.Message)
        {
            Id = apiError.Id;
            StatusCode = apiError.StatusCode;
        }

        public Urn Id { get; }
        public HttpStatusCode StatusCode { get; }
    }
}