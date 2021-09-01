#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model.DraftBuilders
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
        public static readonly Urn Namespace = new("urn:drafts-builder");
        
        private static readonly Urn LegacyNamespace = new("urn:externapi");
        
        private readonly Urn? urn;

        public DraftBuilderType(string urn)
            : this(new Urn(urn))
        {
        }

        public DraftBuilderType(Urn urn)
        {
            this.urn = urn ?? throw new ArgumentNullException(nameof(urn));
            if (!Namespace.IsParentOf(urn) && !LegacyNamespace.IsParentOf(urn))
                throw Errors.UrnDoesNotBelongToNamespace(nameof(urn), urn, Namespace);
        }

        public Urn? ToUrn() => urn;

        public override string ToString() => urn?.ToString() ?? string.Empty;

        public bool Equals(DraftBuilderType other) => Equals(urn, other.urn);

        public override bool Equals(object? obj) => obj is DraftBuilderType other && Equals(other);

        public override int GetHashCode() => urn != null ? urn.GetHashCode() : 0;

        public static bool operator==(DraftBuilderType left, DraftBuilderType right) => left.Equals(right);

        public static bool operator!=(DraftBuilderType left, DraftBuilderType right) => !left.Equals(right);

        public static implicit operator DraftBuilderType(string value) => new(value);
    }
}