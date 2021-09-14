#nullable enable
using System;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Exceptions;

#nullable enable

namespace Kontur.Extern.Api.Client.Model
{
    public class CertificateContent
    {
        public static CertificateContent FromBytes(byte[] bytes)
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
        
        private CertificateContent(byte[] bytes) => this.bytes = bytes;

        public byte[] ToBytes() => bytes;

        public static implicit operator CertificateContent(byte[] bytes) => FromBytes(bytes);
    }
}