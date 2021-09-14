#nullable enable
using System;
using System.Text;

namespace Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.Extensions
{
    public readonly struct Utf8String
    {
        private readonly string? value;
        private readonly ReadOnlyMemory<byte> utf8Value;

        public Utf8String(string value)
        {
            this.value = value;
            utf8Value = Encoding.UTF8.GetBytes(value);
        }

        public ReadOnlySpan<byte> AsUtf8() => utf8Value.Span;

        public override string ToString() => value ?? string.Empty;

        public static implicit operator Utf8String(string value) => new(value);
    }
}