using System.Collections.Generic;
using System.Net;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Errors
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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
        }

        public new Urn Id { get; set; }
        public new HttpStatusCode StatusCode { get; set; }
        public new string Message { get; set; }
        public new string TrackId { get; set; }
        public string TraceId { get; set; }
        public new Dictionary<string, string> Properties { get; set; }

        public override string ToString() =>
            $"[id: \"{Id}\", status: {StatusCode}, track-id: \"{TrackId}\", trace-id: \"{TraceId}\"]";
    }
}