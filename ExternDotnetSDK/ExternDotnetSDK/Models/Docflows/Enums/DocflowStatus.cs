#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.Docflows.Enums
{
    /// <summary>
    /// Статус документооборота. Подробнее о значении статуса читайте в [документации](https://docs-ke.readthedocs.io/ru/latest/specification/%D1%81%D1%82%D0%B0%D1%82%D1%83%D1%81%D1%8B%20%D0%94%D0%9E.html)
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct DocflowStatus
    {
        /// <summary>
        /// Пространство имен статусов документооборота
        /// </summary>
        public static readonly Urn Namespace = Urn.Parse("urn:docflow-common-status");
        
        private readonly Urn? urn;

        public DocflowStatus(string urn)
            : this(Urn.Parse(urn))
        {
        }

        public DocflowStatus(Urn urn)
        {
            this.urn = urn ?? throw new ArgumentNullException(nameof(urn));
            if (!Namespace.IsParentOf(urn))
                throw Errors.UrnDoesNotBelongToNamespace(nameof(urn), urn, Namespace);
        }

        [Pure]
        public Urn? ToUrn() => urn;

        public override string ToString() => urn?.ToString() ?? string.Empty;

        public bool Equals(DocflowStatus other) => Equals(urn, other.urn);

        public override bool Equals(object? obj) => obj is DocflowStatus other && Equals(other);

        public override int GetHashCode() => urn != null ? urn.GetHashCode() : 0;

        public static bool operator==(DocflowStatus left, DocflowStatus right) => left.Equals(right);

        public static bool operator!=(DocflowStatus left, DocflowStatus right) => !left.Equals(right);

        public static implicit operator DocflowStatus(string value) => new(value);
    }
}