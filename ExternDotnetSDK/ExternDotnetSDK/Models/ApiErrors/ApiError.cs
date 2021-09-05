#nullable enable
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Kontur.Extern.Client.Models.Common;

namespace Kontur.Extern.Client.Models.ApiErrors
{
    public class ApiError
    {
        public static readonly Urn Namespace = new("urn:error");
        
        public ApiError(HttpStatusCode statusCode, string? message = null, string? traceId = null, Dictionary<string, string>? properties = null)
            : this(Namespace, statusCode, message, traceId, properties)
        {
        }
        
        [JsonConstructor]
        public ApiError(Urn id, HttpStatusCode statusCode, string? message = null, string? traceId = null, Dictionary<string, string>? properties = null)
        {
            Id = id;
            StatusCode = statusCode;
            Message = message;
            TraceId = traceId;
            Properties = properties ?? new Dictionary<string, string>();
        }

        public Urn Id { get; }
        public HttpStatusCode StatusCode { get; }
        public string? Message { get; }
        public string? TraceId { get; }
        public Dictionary<string, string> Properties { get; }

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