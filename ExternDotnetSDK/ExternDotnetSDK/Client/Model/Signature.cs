using System;
using Kontur.Extern.Client.Common;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model
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
    }
}