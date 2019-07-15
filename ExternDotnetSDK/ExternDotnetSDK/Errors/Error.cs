using System.Collections.Generic;
using System.Net;
using ExternDotnetSDK.Common;

namespace ExternDotnetSDK.Errors
{
    public class Error
    {
        public static readonly Urn Namespace = new Urn("urn:error");

        public Error()
            : this(Error.Namespace, HttpStatusCode.InternalServerError, (string) null)
        {
        }

        public Error(Urn id, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = null)
        {
            this.Id = id;
            this.StatusCode = statusCode;
            this.Message = message;
            this.Properties = new Dictionary<string, string>();
        }

        public Urn Id { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public string TrackId { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public override string ToString()
        {
            return string.Format("[id: \"{0}\", status: {1}, track-id: \"{2}\"]", (object) this.Id, (object) this.StatusCode, (object) this.TrackId);
        }
    }
}