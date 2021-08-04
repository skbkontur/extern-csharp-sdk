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

        public static implicit operator DocumentType(string value) => new(value);
    }
}