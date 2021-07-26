#nullable enable
using System;

namespace Kontur.Extern.Client.Http.Models
{
    public readonly struct UrlEncodedString
    {
        private readonly string? value;

        public UrlEncodedString(string value) => this.value = Encode(value);

        public bool IsEmpty => string.IsNullOrEmpty(value);

        public override string ToString() => value ?? string.Empty;

        public static implicit operator UrlEncodedString(string value) => new(value);

        private static string Encode(string data)
        {
            if (string.IsNullOrEmpty(data))
                return string.Empty;
            return Uri.EscapeDataString(data).Replace("%20", "+");
        }
    }
}