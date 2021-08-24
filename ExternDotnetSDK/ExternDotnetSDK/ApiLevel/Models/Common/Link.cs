using System;
using System.Text;

namespace Kontur.Extern.Client.ApiLevel.Models.Common
{
    public sealed class Link : IEquatable<Link>
    {
        public const string RelSelf = "self";
        public const string RelPrev = "prev";
        public const string RelNext = "next";

        public readonly Uri Href;
        public readonly string Rel;
        public readonly string Name;
        public readonly string Title;
        public readonly string Profile;
        public readonly bool Templated;

        public Link(Uri href, string rel, string name = null, string title = null, string profile = null, bool templated = false)
        {
            Href = href ?? throw new ArgumentNullException(nameof(href));
            Rel = rel ?? throw new ArgumentNullException(nameof(rel));
            Name = name;
            Title = title;
            Templated = templated;
            Profile = profile;
        }

        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is Link link && Equals(link);

        public bool Equals(Link other) =>
            other != null &&
            Href == other.Href &&
            EqualsOrNulls(Rel, other.Rel) &&
            EqualsOrNulls(Title, other.Title) &&
            EqualsOrNulls(Name, other.Name) &&
            EqualsOrNulls(Profile, other.Profile) &&
            Templated == other.Templated;

        public override string ToString()
        {
            var builder = new StringBuilder("<link ");
            TryAppendLinkPart(builder, "rel", Rel);
            TryAppendLinkPart(builder, "name", Name);
            TryAppendLinkPart(builder, "profile", Profile);
            TryAppendLinkPart(builder, "title", Title);
            if (Templated)
                builder.Append("templated=\"true\" ");
            builder.Append($"href=\"{Href}\" />");
            return builder.ToString();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashcode = 19*23 + Href.GetHashCode();
                hashcode = TryIncreaseHashcode(hashcode, Title);
                hashcode = TryIncreaseHashcode(hashcode, Rel);
                hashcode = TryIncreaseHashcode(hashcode, Name);
                hashcode = TryIncreaseHashcode(hashcode, Profile);
                return hashcode*23 + Templated.GetHashCode();
            }
        }

        private void TryAppendLinkPart(StringBuilder builder, string parameterName, string parameter)
        {
            if (!string.IsNullOrWhiteSpace(parameter))
                builder.Append($"{parameterName}=\"{parameter}\" ");
        }

        private static bool EqualsOrNulls(string a, string b) =>
            a == null
                ? b == null
                : b != null && string.Compare(a, b, StringComparison.CurrentCultureIgnoreCase) == 0;

        private int TryIncreaseHashcode(int hashcode, string linkField)
        {
            unchecked
            {
                return linkField is null
                    ? hashcode
                    : hashcode*23 + linkField.ToLower().GetHashCode();
            }
        }
    }
}