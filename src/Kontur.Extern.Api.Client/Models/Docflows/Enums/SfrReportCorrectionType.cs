using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.Docflows.Enums
{
    /// <summary>
    /// Тип корректировки
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct SfrReportCorrectionType
    {
        private static Urn? @namespace;
        public static Urn Namespace => @namespace ??= Urn.Parse("urn:sfr-report-correction-type");

        private readonly Urn urn;

        public SfrReportCorrectionType(string urn)
            : this(Urn.Parse(urn))
        {
        }

        public SfrReportCorrectionType(Urn urn)
        {
            this.urn = urn ?? throw new ArgumentNullException(nameof(urn));
            if (!Namespace.IsParentOf(urn))
                throw Errors.UrnDoesNotBelongToNamespace(nameof(urn), urn, Namespace);
        }

        [Pure]
        public Urn ToUrn() => urn;

        public override string ToString() => urn.ToString();

        public bool Equals(SfrReportCorrectionType other) => Equals(urn, other.urn);

        public override bool Equals(object? obj) => obj is SfrReportCorrectionType other && Equals(other);

        public override int GetHashCode() => urn.GetHashCode();

        public static bool operator==(SfrReportCorrectionType left, SfrReportCorrectionType right) => left.Equals(right);

        public static bool operator!=(SfrReportCorrectionType left, SfrReportCorrectionType right) => !left.Equals(right);

        public static implicit operator SfrReportCorrectionType(string value) => new(value);
    }
}