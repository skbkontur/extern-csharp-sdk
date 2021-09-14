using System;
using System.Text;
using Kontur.Extern.Api.Client.Http.Exceptions;

namespace Kontur.Extern.Api.Client.Http.Models
{
    public readonly struct Base64String
    {
        public static Base64String FromEncoded(string encoded)
        {
            if (string.IsNullOrWhiteSpace(encoded))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(encoded));
            return new(encoded);
        }

        public static Base64String Encode(byte[] bytes) => 
            new(Convert.ToBase64String(bytes ?? throw new ArgumentNullException(nameof(bytes))));
        public static Base64String Encode(string plainText)
        {
            if (plainText == null)
                throw new ArgumentNullException(nameof(plainText));

            return new(Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText)));
        }

        private readonly string value;

        private Base64String(string encoded) => value = encoded;

        public byte[] Decode() => Convert.FromBase64String(value ?? throw new ArgumentNullException(nameof(value)));

        public override string ToString() => value;
    }
}