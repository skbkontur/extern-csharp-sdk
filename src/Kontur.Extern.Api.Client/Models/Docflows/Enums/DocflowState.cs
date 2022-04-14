using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.Docflows.Enums
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct DocflowState
    {
        /// <summary>
        /// Пространство имен состояний документооборота
        /// </summary>
        public static readonly Urn Namespace = Urn.Parse("urn:docflow-state");
        
        private readonly Urn urn;

        public DocflowState(string urn)
            : this(Urn.Parse(urn))
        {
        }

        public DocflowState(Urn urn)
        {
            this.urn = urn ?? throw new ArgumentNullException(nameof(urn));
            if (!Namespace.IsParentOf(urn))
                throw Errors.UrnDoesNotBelongToNamespace(nameof(urn), urn, Namespace);
        }

        [Pure]
        public Urn ToUrn() => urn;

        public override string ToString() => urn.ToString();

        public bool Equals(DocflowState other) => Equals(urn, other.urn);

        public override bool Equals(object? obj) => obj is DocflowState other && Equals(other);

        public override int GetHashCode() => urn.GetHashCode();

        public static bool operator==(DocflowState left, DocflowState right) => left.Equals(right);

        public static bool operator!=(DocflowState left, DocflowState right) => !left.Equals(right);

        public static implicit operator DocflowState(string value) => new(value);
    }
}