using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.Docflows.Enums
{
    /// <summary>
    /// Дополнительный статус документооборота
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct DocflowExtendedStatus
    {
        /// <summary>
        /// Пространство имен статусов документооборота
        /// </summary>
        public static readonly Urn Namespace = Urn.Parse("urn:docflow-extended-status");
        
        private readonly Urn urn;

        public DocflowExtendedStatus(string urn)
            : this(Urn.Parse(urn))
        {
        }

        public DocflowExtendedStatus(Urn urn)
        {
            this.urn = urn ?? throw new ArgumentNullException(nameof(urn));
            if (!Namespace.IsParentOf(urn))
                throw Errors.UrnDoesNotBelongToNamespace(nameof(urn), urn, Namespace);
        }

        [Pure]
        public Urn ToUrn() => urn;

        public override string ToString() => urn.ToString();

        public bool Equals(DocflowExtendedStatus other) => Equals(urn, other.urn);

        public override bool Equals(object? obj) => obj is DocflowExtendedStatus other && Equals(other);

        public override int GetHashCode() => urn.GetHashCode();

        public static bool operator==(DocflowExtendedStatus left, DocflowExtendedStatus right) => left.Equals(right);

        public static bool operator!=(DocflowExtendedStatus left, DocflowExtendedStatus right) => !left.Equals(right);

        public static implicit operator DocflowExtendedStatus(string value) => new(value);
    }
}