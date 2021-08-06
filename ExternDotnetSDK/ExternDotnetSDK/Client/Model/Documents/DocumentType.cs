#nullable enable
using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.Model.Documents
{
    [PublicAPI]
    public readonly partial struct DocumentType
    {
        private readonly Urn? urn;

        public DocumentType(string urn) => this.urn = new Urn(urn);

        public Urn? ToUrn() => urn;

        public override string ToString() => urn?.ToString() ?? string.Empty;

        public bool Equals(DocumentType other) => Equals(urn, other.urn);

        public override bool Equals(object? obj) => obj is DocumentType other && Equals(other);

        public override int GetHashCode() => urn != null ? urn.GetHashCode() : 0;

        public static bool operator==(DocumentType left, DocumentType right) => left.Equals(right);

        public static bool operator!=(DocumentType left, DocumentType right) => !left.Equals(right);

        public static implicit operator DocumentType(string value) => new(value);
    }
}