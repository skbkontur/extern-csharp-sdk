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
        throw new ArgumentNullException(nameof (value));
      if (string.Compare(value, 0, "urn:", 0, "urn:".Length, StringComparison.OrdinalIgnoreCase) != 0)
        throw new UrnException("Invalid URN schema");
      this.Value = value.Substring("urn:".Length);
    }

    public Urn(string nid, string nss)
    {
      if (nid == null)
        throw new ArgumentNullException(nameof (nid));
      if (nss == null)
        throw new ArgumentNullException(nameof (nss));
      this.Value = nid + ":" + nss;
    }

    public Urn(Urn parent, string nss)
    {
      if (parent == (Urn) null)
        throw new ArgumentNullException(nameof (parent));
      if (nss == null)
        throw new ArgumentNullException(nameof (nss));
      this.Value = parent.Value + ":" + nss;
    }

    public string Nid
    {
      get
      {
        int length = this.Value.LastIndexOf(':');
        if (length == -1)
          return string.Empty;
        return this.Value.Substring(0, length);
      }
    }

    public string Nss
    {
      get
      {
        int num = this.Value.LastIndexOf(':');
        if (num == -1)
          return this.Value;
        return this.Value.Substring(num + 1);
      }
    }

    public string Value { get; private set; }

    public int CompareTo(Urn other)
    {
      if (other == (Urn) null)
        return 1;
      return string.Compare(this.Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    public bool Equals(Urn other)
    {
      if ((object) other != null)
        return string.Compare(this.Value, other.Value, StringComparison.OrdinalIgnoreCase) == 0;
      return false;
    }

    public static Urn Parse(string value)
    {
      return new Urn(value);
    }

    public static bool TryParse(string value, out Urn result)
    {
      result = (Urn) null;
      if (value == null || !value.ToLower().StartsWith("urn:"))
        return false;
      result = Urn.Parse(value);
      return true;
    }

    public static bool operator ==(Urn a, Urn b)
    {
      if ((object) a == (object) b)
        return true;
      if ((object) a == null)
        return false;
      return a.Equals(b);
    }

    public static bool operator !=(Urn a, Urn b)
    {
      return !(a == b);
    }

    public Urn CreateChild(string nss)
    {
      return new Urn(this, nss);
    }

    public bool IsParentOf(Urn urn)
    {
      if (urn == (Urn) null)
        throw new ArgumentNullException(nameof (urn));
      if (urn.Value.Length <= this.Value.Length || string.Compare(this.Value, 0, urn.Value, 0, this.Value.Length, StringComparison.OrdinalIgnoreCase) != 0)
        return false;
      return urn.Value[this.Value.Length] == ':';
    }

    public bool IsChildOf(Urn urn)
    {
      if (urn == (Urn) null)
        throw new ArgumentNullException(nameof (urn));
      return urn.IsParentOf(this);
    }

    public override int GetHashCode()
    {
      return this.Value.ToLowerInvariant().GetHashCode();
    }

    public override bool Equals(object obj)
    {
      return this.Equals(obj as Urn);
    }

    public override string ToString()
    {
      return "urn:" + this.Value;
    }
  }
}
