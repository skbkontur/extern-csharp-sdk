#nullable enable
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Kontur.Extern.Api.Client.Models.Common
{
    public sealed class Link : IEquatable<Link>
    {
        [JsonConstructor]
        public Link(Uri href, string rel, string? name = null, string? title = null, string? profile = null, bool templated = false)
        {
            Href = href ?? throw new ArgumentNullException(nameof(href));
            Rel = rel ?? throw new ArgumentNullException(nameof(rel));
            Name = name;
            Title = title;
            Templated = templated;
            Profile = profile;
        }
        
        public Uri Href { get; }
        public string Rel { get; }
        public string? Name { get; }
        public string? Title { get; }
        public string? Profile { get; }
        public bool Templated { get; }

        public override bool Equals(object obj) => 
            ReferenceEquals(this, obj) || obj is Link link && Equals(link);

        public bool Equals(Link? other)
        {
            return other is not null &&
                   Href == other.Href &&
                   EqualsOrNulls(Rel, other.Rel) &&
                   EqualsOrNulls(Title, other.Title) &&
                   EqualsOrNulls(Name, other.Name) &&
                   EqualsOrNulls(Profile, other.Profile) &&
                   Templated == other.Templated;
            
            static bool EqualsOrNulls(string? string1, string? string2) =>
                string1 is null
                    ? string2 is null
                    : string2 is not null && string1.Equals(string2, StringComparison.CurrentCultureIgnoreCase);
        }

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
            
            static void TryAppendLinkPart(StringBuilder builder, string parameterName, string? parameter)
            {
                if (!string.IsNullOrWhiteSpace(parameter))
                    builder.Append($"{parameterName}=\"{parameter}\" ");
            }
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
            
            static int TryIncreaseHashcode(int hashcode, string? linkField)
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
}