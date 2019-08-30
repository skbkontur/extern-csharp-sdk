using System;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Common
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    [JsonConverter(typeof (UrnJsonConverter))]
    public sealed class Urn : IComparable<Urn>, IEquatable<Urn>
    {
        private const string Schema = "urn:";

        public static Urn Parse(string value) => new Urn(value);

        public static bool TryParse(string value, out Urn result)
        {
            result = null;
            if (value?.ToLower().StartsWith(Schema, StringComparison.Ordinal) != true)
                return false;
            result = Parse(value);
            return true;
        }

        public Urn(string value)
        {
            TryThrowArgumentNullException(value);
            if (0 != string.Compare(value, 0, Schema, 0, Schema.Length, StringComparison.OrdinalIgnoreCase))
                throw new UrnException("Invalid URN schema");
            Value = value.Substring(Schema.Length);
        }

        [JsonConstructor]
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

        public int CompareTo(Urn other) =>
            other == null ? 1 : string.Compare(Value, other.Value, StringComparison.OrdinalIgnoreCase);

        public bool Equals(Urn other) =>
            !(other is null) && 0 == string.Compare(Value, other.Value, StringComparison.OrdinalIgnoreCase);

        public Urn CreateChild(string nss) => new Urn(this, nss);

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

        public static bool operator==(Urn a, Urn b) => ReferenceEquals(a, b) || a?.Equals(b) == true;

        public static bool operator!=(Urn a, Urn b) => !(a == b);

        public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();

        public override bool Equals(object obj) => Equals(obj as Urn);

        public override string ToString() => Schema + Value;

        private void TryThrowArgumentNullException(Urn urn)
        {
            if (urn == null)
                throw new ArgumentNullException(nameof(urn));
        }

        private void TryThrowArgumentNullException(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(nameof(str));
        }
    }
}