using System.Collections.Generic;
using System.Net;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Errors
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Error
    {
        public static readonly Urn Namespace = new Urn("urn:error");

        public Error()
            : this(Namespace)
        {
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
        public Dictionary<string, string> Properties { get; set; }

        public override string ToString()
        {
            return $"[id: \"{Id}\", status: {StatusCode}, track-id: \"{TrackId}\"]";
        }
    }
}