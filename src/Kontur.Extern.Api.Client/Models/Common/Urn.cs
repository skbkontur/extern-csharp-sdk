using System;
using Kontur.Extern.Api.Client.Exceptions;

namespace Kontur.Extern.Api.Client.Models.Common
{
    public sealed class Urn : IComparable<Urn>, IEquatable<Urn>
    {
        private const string Schema = "urn:";

        public static Urn Parse(string value)
        {
            TryThrowArgumentNullException(value);
            if (0 != string.Compare(value, 0, Schema, 0, Schema.Length, StringComparison.OrdinalIgnoreCase))
                throw Errors.InvalidUrnSchema(nameof(value), value);
            if (char.IsWhiteSpace(value[value.Length - 1]))
                throw Errors.UrnCannotHaveTrailingWhitespaces(nameof(value), value);
            
            return new(value);
        }

        public static bool TryParse(string? value, out Urn result)
        {
            if (value?.ToLower().StartsWith(Schema, StringComparison.Ordinal) != true ||
                char.IsWhiteSpace(value[value.Length - 1]))
            {
                result = null!;
                return false;
            }
            
            result = new Urn(value);
            return true;
        }

        public Urn(string nid, string nss)
        {
            TryThrowArgumentNullException(nid);
            TryThrowArgumentNullException(nss);
            Value = $"{nid}:{nss}";
        }

        public Urn(Urn parent, string nss)
        {
            TryThrowArgumentNullException(parent);
            TryThrowArgumentNullException(nss);
            Value = $"{parent.Value}:{nss}";
        }

        private Urn(string value) => 
            Value = value.Substring(Schema.Length);

        public string Value { get; }

        public string Nid
        {
            get
            {
                var index = Value.LastIndexOf(':');
                return index != -1 ? Value.Substring(0, index) : string.Empty;
            }
        }

        public string Nss
        {
            get
            {
                var index = Value.LastIndexOf(':');
                return index != -1 ? Value.Substring(index + 1) : Value;
            }
        }

        public Urn Append(string nss) => new(this, nss);

        public bool IsParentOf(Urn urn)
        {
            TryThrowArgumentNullException(urn);
            return urn.Value.Length > Value.Length &&
                   0 == string.Compare(Value, 0, urn.Value, 0, Value.Length, StringComparison.OrdinalIgnoreCase) &&
                   urn.Value[Value.Length] == ':';
        }

        public bool IsChildOf(Urn urn)
        {
            TryThrowArgumentNullException(urn);
            return urn.IsParentOf(this);
        }
        
        public int CompareTo(Urn? other) =>
            other is null ? 1 : string.Compare(Value, other.Value, StringComparison.OrdinalIgnoreCase);

        public bool Equals(Urn? other) =>
            other is not null && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);

        public static bool operator==(Urn? a, Urn? b) => ReferenceEquals(a, b) || a?.Equals(b) == true;

        public static bool operator!=(Urn? a, Urn? b) => !(a == b);

        public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();

        public override bool Equals(object obj) => Equals(obj as Urn);

        public override string ToString() => Schema + Value;

        private static void TryThrowArgumentNullException(Urn? urn)
        {
            if (urn is null)
                throw new ArgumentNullException(nameof(urn));
        }

        private static void TryThrowArgumentNullException(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(nameof(str));
        }
    }
}