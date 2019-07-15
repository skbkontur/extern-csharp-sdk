// Decompiled with JetBrains decompiler
// Type: Kontur.Api.Urn
// Assembly: Kontur.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AA280C05-1D50-4E0C-9593-3BE7096E3298
// Assembly location: C:\Users\trubitsin\Desktop\Not My Projects\ke.api\libapi\Kontur.Api\bin\Release\Kontur.Api.dll

using System;

namespace ExternDotnetSDK.Common
{
    public sealed class Urn : IComparable<Urn>, IEquatable<Urn>
    {
        private const string Schema = "urn:";

        public Urn(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (0 != string.Compare(value, 0, Schema, 0, Schema.Length, StringComparison.OrdinalIgnoreCase))
                throw new UrnException("Invalid URN schema");

            this.Value = value.Substring(Schema.Length);
        }

        public Urn(string nid, string nss)
        {
            if (nid == null)
                throw new ArgumentNullException(nameof(nid));

            if (nss == null)
                throw new ArgumentNullException(nameof(nss));

            Value = nid + ':' + nss;
        }

        public Urn(Urn parent, string nss)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            if (nss == null)
                throw new ArgumentNullException(nameof(nss));

            Value = parent.Value + ':' + nss;
        }

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

        public string Value { get; private set; }

        public int CompareTo(Urn other)
        {
            if (other == null)
                return 1;

            return string.Compare(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(Urn other)
        {
            return ((object) other) != null && 0 == string.Compare(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public static Urn Parse(string value)
        {
            return new Urn(value);
        }

        public static bool TryParse(string value, out Urn result)
        {
            result = null;

            if (value == null || !value.ToLower().StartsWith(Schema))
                return false;

            result = Parse(value);
            return true;
        }

        public static bool operator==(Urn a, Urn b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if ((object) a == null)
                return false;

            return a.Equals(b);
        }

        public static bool operator!=(Urn a, Urn b)
        {
            return !(a == b);
        }

        public Urn CreateChild(string nss)
        {
            return new Urn(this, nss);
        }

        public bool IsParentOf(Urn urn)
        {
            if (urn == null)
                throw new ArgumentNullException(nameof(urn));

            if (urn.Value.Length <= Value.Length)
                return false;

            if (0 != string.Compare(Value, 0, urn.Value, 0, Value.Length, StringComparison.OrdinalIgnoreCase))
                return false;

            return urn.Value[Value.Length] == ':';
        }

        public bool IsChildOf(Urn urn)
        {
            if (urn == null)
                throw new ArgumentNullException(nameof(urn));

            return urn.IsParentOf(this);
        }

        public override int GetHashCode()
        {
            return Value.ToLowerInvariant().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Urn);
        }

        public override string ToString()
        {
            return Schema + Value;
        }
    }
}