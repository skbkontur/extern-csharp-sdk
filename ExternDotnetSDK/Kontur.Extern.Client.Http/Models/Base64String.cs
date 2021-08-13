using System;
using System.Text;

namespace Kontur.Extern.Client.Http.Models
{
    public readonly struct Base64String
    {
        public static Base64String FromEncoded(string encoded) => new(encoded);
        public static Base64String Encode(byte[] bytes) => new(Convert.ToBase64String(bytes));
        public static Base64String Encode(string plainText) => new(Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText)));
        
        private readonly string value;

        private Base64String(string encoded) => value = encoded;

        public byte[] Decode() => Convert.FromBase64String(value ?? throw new ArgumentNullException(nameof(value)));

        public override string ToString() => value;
    }
}