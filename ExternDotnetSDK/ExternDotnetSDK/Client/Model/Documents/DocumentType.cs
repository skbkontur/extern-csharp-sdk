#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public readonly partial struct DocumentType
    {
        /// <summary>
        /// Пространство имен типов документов
        /// </summary>
        public static readonly Urn Namespace = new("urn:document");
        
        private readonly Urn? urn;

        public DocumentType(string urn)
            : this(new Urn(urn))
        {
        }

        public DocumentType(Urn urn)
        {
            this.urn = urn ?? throw new ArgumentNullException(nameof(urn));
            if (!Namespace.IsParentOf(urn))
                throw Errors.UrnDoesNotBelongToNamespace(nameof(urn), urn, Namespace);
        }

        public Urn? ToUrn() => urn;

        public bool IsBelongTo(Urn @namespace)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (@namespace is null)
                return false;
            
            return urn is not null && urn.Value.StartsWith(@namespace.Value);
        }

        public override string ToString() => urn?.ToString() ?? string.Empty;

        public bool Equals(DocumentType other) => Equals(urn, other.urn);

        public override bool Equals(object? obj) => obj is DocumentType other && Equals(other);

        public override int GetHashCode() => urn != null ? urn.GetHashCode() : 0;

        public static bool operator==(DocumentType left, DocumentType right) => left.Equals(right);

        public static bool operator!=(DocumentType left, DocumentType right) => !left.Equals(right);

        public static implicit operator DocumentType(string value) => new(value);
    }
}