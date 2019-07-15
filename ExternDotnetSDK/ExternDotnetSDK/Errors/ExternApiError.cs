using System.Collections.Generic;
using System.Net;
using ExternDotnetSDK.Common;

namespace ExternDotnetSDK.Errors
{
    public class ExternApiError : Error
    {
        public new static readonly Urn Namespace = new Urn("urn:error:externapi");

        public ExternApiError()
            : this(Namespace)
        {
        }

        public ExternApiError(Urn id, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = null)
            : base(id, statusCode, message)
        {
            //TraceId = LoggingContext.Prefix;
        }

        public new Urn Id { get; set; }
        public new HttpStatusCode StatusCode { get; set; }
        public new string Message { get; set; }
        public new string TrackId { get; set; }
        public string TraceId { get; set; }

        public new Dictionary<string, string> Properties { get; set; }

        public override string ToString()
        {
            return $"[id: \"{Id}\", status: {StatusCode}, track-id: \"{TrackId}\", trace-id: \"{TraceId}\"]";
        }
    }
}
