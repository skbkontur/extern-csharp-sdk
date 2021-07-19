using System;
using System.Text;

namespace Kontur.Extern.Client.HttpLevel.Models
{
    public readonly struct Base64String
    {
        public static Base64String Encode(string plainText) => new(plainText);
        
        private readonly string value;

        private Base64String(string plainText) => value = Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

        public override string ToString() => value;
    }
}