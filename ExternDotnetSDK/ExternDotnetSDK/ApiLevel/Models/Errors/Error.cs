using System.Collections.Generic;
using System.Net;
using System.Text;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Errors
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Error
    {
        public static readonly Urn Namespace = new("urn:error");

        public Error()
            : this(Namespace)
        {
        }

        public Error(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
            Properties = new Dictionary<string, string>();
        }
        
        public Error(Urn id, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = null)
        {
            Id = id;
            StatusCode = statusCode;
            Message = message;
            Properties = new Dictionary<string, string>();
        }

        public Urn Id { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string TrackId { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string, string> Properties { get; set; }

        public bool IsNotEmpty => !string.IsNullOrWhiteSpace(Message);

        public override string ToString()
        {
            var text = new StringBuilder();
            text.Append($"[id: \"{Id}\", status: {StatusCode}");
            if (!string.IsNullOrWhiteSpace(TrackId))
            {
                text.Append($", track-id: \"{TrackId}\"");
            }
            if (!string.IsNullOrWhiteSpace(TraceId))
            {
                text.Append($", trace-id: \"{TraceId}\"");
            }
            text.AppendLine("]");
            text.Append(Message);
            return text.ToString();
        }

        public Error ReplaceMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw Exceptions.Errors.StringShouldNotBeNullOrWhiteSpace(nameof(message));

            return new Error
            {
                Id = Id,
                Message = message,
                Properties = Properties,
                StatusCode = StatusCode,
                TrackId = TrackId
            };
        }
    }
}