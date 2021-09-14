using System;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Http.Models;

namespace Kontur.Extern.Api.Client.Model
{
    public class Signature
    {
        public static Signature FromBytes(byte[] bytes)
        {
            if (bytes is null)
                throw new ArgumentNullException(nameof(bytes));
            if (bytes.Length == 0)
                throw Errors.ArrayCannotBeEmpty(nameof(bytes));

            if (BytesComparer.IsZero(bytes))
                throw Errors.BytesArrayCannotContainsOnlyZeros(nameof(bytes));

            return new(bytes);
        }
        
        private readonly byte[] bytes;
        
        private Signature(byte[] bytes) => this.bytes = bytes;

        public byte[] ToBytes() => bytes;

        public static implicit operator Signature(byte[] bytes) => FromBytes(bytes);
        
        public static implicit operator Signature(Base64String base64String) => FromBytes(base64String.Decode());

        public Base64String ToBase64String() => Base64String.Encode(bytes);
    }
}