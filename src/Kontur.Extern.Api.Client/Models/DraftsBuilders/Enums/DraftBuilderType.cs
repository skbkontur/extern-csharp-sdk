using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums
{
    /// <summary>
    /// Типы DraftsBuilder
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public readonly partial struct DraftBuilderType
    {
        /// <summary>
        /// Пространство имен типов документов
        /// </summary>
        public static readonly Urn Namespace = Urn.Parse("urn:drafts-builder");
        
        private readonly Urn urn;

        public DraftBuilderType(string urn)
            : this(Urn.Parse(urn))
        {
        }

        public DraftBuilderType(Urn urn)
        {
            this.urn = urn ?? throw new ArgumentNullException(nameof(urn));
            if (!Namespace.IsParentOf(urn))
                throw Errors.UrnDoesNotBelongToNamespace(nameof(urn), urn, Namespace);
        }

        internal bool IsEmpty => urn is null;

        public Urn ToUrn() => urn;

        public override string ToString() => urn.ToString();

        public bool Equals(DraftBuilderType other) => Equals(urn, other.urn);

        public override bool Equals(object? obj) => obj is DraftBuilderType other && Equals(other);

        public override int GetHashCode() => urn.GetHashCode();

        public static bool operator==(DraftBuilderType left, DraftBuilderType right) => left.Equals(right);

        public static bool operator!=(DraftBuilderType left, DraftBuilderType right) => !left.Equals(right);

        public static implicit operator DraftBuilderType(string value) => new(value);
    }
}