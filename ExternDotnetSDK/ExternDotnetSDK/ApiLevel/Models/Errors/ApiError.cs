using System.Collections.Generic;
using System.Net;
using System.Text;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Errors
{
    public class ApiError
    {
        public static readonly Urn Namespace = new("urn:error");

        public ApiError()
            : this(Namespace)
        {
        }

        public ApiError(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
            Properties = new Dictionary<string, string>();
        }
        
        public ApiError(Urn id, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = null)
        {
            Id = id;
            StatusCode = statusCode;
            Message = message;
            Properties = new Dictionary<string, string>();
        }

        public Urn Id { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string, string> Properties { get; set; }

        public bool IsNotEmpty => !string.IsNullOrWhiteSpace(Message);

        public override string ToString()
        {
            var text = new StringBuilder();
            text.Append($"[id: \"{Id}\", status: {StatusCode}");
            if (!string.IsNullOrWhiteSpace(TraceId))
            {
                text.Append($", trace-id: \"{TraceId}\"");
            }
            text.AppendLine("]");
            text.Append(Message);
            return text.ToString();
        }
    }
}