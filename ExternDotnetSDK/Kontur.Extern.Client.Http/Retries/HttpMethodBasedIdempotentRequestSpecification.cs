using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Http.Retries
{
    public class HttpMethodBasedIdempotentRequestSpecification : IIdempotentRequestSpecification
    {
        private readonly HashSet<string> idempotentHttpMethods;
        public static readonly HttpMethodBasedIdempotentRequestSpecification SemanticallyIdempotentMethods = new(new[]
        {
            HttpMethod.Get,
            HttpMethod.Put,
            HttpMethod.Delete,
            HttpMethod.Head,
            HttpMethod.Options,
            HttpMethod.Trace
        });
        
        public static readonly HttpMethodBasedIdempotentRequestSpecification OnlyGetMethodIsIdempotent = new(new[]
        {
            HttpMethod.Get
        });

        public HttpMethodBasedIdempotentRequestSpecification(IEnumerable<HttpMethod> idempotentHttpMethods)
        {
            var methodNames = idempotentHttpMethods.Select(m => m.ToString());
            this.idempotentHttpMethods = new HashSet<string>(methodNames, StringComparer.OrdinalIgnoreCase);
        }

        public IEnumerable<string> IdempotentHttpMethods => idempotentHttpMethods;

        public bool IsIdempotent(Request request) => 
            idempotentHttpMethods.Contains(request.Method);
    }
}