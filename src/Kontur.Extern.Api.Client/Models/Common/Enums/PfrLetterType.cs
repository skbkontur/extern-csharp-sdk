using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;

namespace Kontur.Extern.Api.Client.Models.Common.Enums
{
    /// <summary>
    /// Тип пиьма ПФР, документооборота по неформализованной переписке страхователя и ПФР.
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct PfrLetterType
    {
        /// <summary>
        /// Пространство имен типов писем
        /// </summary>
        public static readonly Urn Namespace = Urn.Parse("urn:pfr-letter-category");
        
        private readonly Urn? urn;

        public PfrLetterType(string urn)
            : this(Urn.Parse(urn))
        {
        }

        public PfrLetterType(Urn urn)
        {
            this.urn = urn ?? throw new ArgumentNullException(nameof(urn));
            if (!Namespace.IsParentOf(urn))
                throw Errors.UrnDoesNotBelongToNamespace(nameof(urn), urn, Namespace);
        }

        [Pure]
        public Urn? ToUrn() => urn;

        public override string ToString() => urn?.ToString() ?? string.Empty;

        public bool Equals(PfrLetterType other) => Equals(urn, other.urn);

        public override bool Equals(object? obj) => obj is PfrLetterType other && Equals(other);

        public override int GetHashCode() => urn != null ? urn.GetHashCode() : 0;

        public static bool operator==(PfrLetterType left, PfrLetterType right) => left.Equals(right);

        public static bool operator!=(PfrLetterType left, PfrLetterType right) => !left.Equals(right);

        public static implicit operator PfrLetterType(string value) => new(value);
    }
}