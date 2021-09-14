using System;
using System.Diagnostics;
using Kontur.Extern.Api.Client.Http.Constants;

namespace Kontur.Extern.Api.Client.Http.Models.Headers
{
    [DebuggerDisplay("{value}")]
    public readonly struct ContentType
    {
        private readonly string? value;

        public ContentType(string value) => this.value = value ?? throw new ArgumentNullException(nameof(value));

        public bool IsJson => IsContentType(ContentTypes.Json);
        public bool IsPlainText => IsContentType(ContentTypes.PlainText);

        public override string ToString() => value ?? string.Empty;

        private bool IsContentType(string contentType) => value != null && value.StartsWith(contentType, StringComparison.OrdinalIgnoreCase);
    }
}