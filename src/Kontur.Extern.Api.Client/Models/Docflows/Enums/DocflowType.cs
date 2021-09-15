using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.Docflows.Enums
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct DocflowType
    {
        /// <summary>
        /// Пространство имен типов документооборотов
        /// </summary>
        public static readonly Urn Namespace = Urn.Parse("urn:docflow");
        
        private readonly Urn? urn;

        public DocflowType(string urn)
            : this(Urn.Parse(urn))
        {
        }

        public DocflowType(Urn urn)
        {
            this.urn = urn ?? throw new ArgumentNullException(nameof(urn));
            if (!Namespace.IsParentOf(urn))
                throw Errors.UrnDoesNotBelongToNamespace(nameof(urn), urn, Namespace);
        }

        [Pure]
        public Urn? ToUrn() => urn;

        public override string ToString() => urn?.ToString() ?? string.Empty;

        public bool Equals(DocflowType other) => Equals(urn, other.urn);

        public override bool Equals(object? obj) => obj is DocflowType other && Equals(other);

        public override int GetHashCode() => urn != null ? urn.GetHashCode() : 0;

        public static bool operator==(DocflowType left, DocflowType right) => left.Equals(right);

        public static bool operator!=(DocflowType left, DocflowType right) => !left.Equals(right);

        public static implicit operator DocflowType(string value) => new(value);
    }
}