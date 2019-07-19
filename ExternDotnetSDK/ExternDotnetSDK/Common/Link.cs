using System;
using System.Text;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Common
{
    /// <summary>Просто ссылка. Подробности на https://wiki.skbkontur.ru/pages/viewpage.action?pageId=82510147. </summary>
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public sealed class Link : IEquatable<Link>
    {
        public const string RelSelf = "self";
        public const string RelPrev = "prev";
        public const string RelNext = "next";

        /// <summary>Ссылка на ресурс.</summary>
        public readonly Uri Href;

        /// <summary>Тип отношения.</summary>
        public readonly string Rel;

        /// <summary>Имя отношения. Используется для идентификации ресурсов с одинаковым типом отношения.</summary>
        public readonly string Name;

        /// <summary>Человек-понятное имя ресурса.</summary>
        public readonly string Title;

        /// <summary>Профиль представления.</summary>
        public readonly string Profile;

        /// <summary>Определяет шаблонную ссылку.</summary>
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

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Link link && Equals(link);
        }

        public bool Equals(Link other)
        {
            return other != null
                   && Href == other.Href &&
                   EqualsOrNulls(Rel, other.Rel) &&
                   EqualsOrNulls(Title, other.Title) &&
                   EqualsOrNulls(Name, other.Name) &&
                   EqualsOrNulls(Profile, other.Profile) &&
                   Templated == other.Templated;
        }

        public override string ToString()
        {
            var builder = new StringBuilder("<link ");
            if (Rel != null)
                builder.Append($"rel=\"{Rel}\" ");
            if (Name != null)
                builder.Append($"name=\"{Name}\" ");
            if (Profile != null)
                builder.Append($"profile=\"{Profile}\" ");
            if (Title != null)
                builder.Append($"title=\"{Title}\" ");
            if (Templated)
                builder.Append("templated=\"true\" ");
            builder.Append($"href=\"{Href}\" />");
            return builder.ToString();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashcode = 19 * 23 + Href.GetHashCode();
                hashcode = TryIncreaseHashcode(hashcode, Title);
                hashcode = TryIncreaseHashcode(hashcode, Rel);
                hashcode = TryIncreaseHashcode(hashcode, Name);
                hashcode = TryIncreaseHashcode(hashcode, Profile);
                return hashcode * 23 + Templated.GetHashCode();
            }
        }

        private static bool EqualsOrNulls(string a, string b)
        {
            return a == null
                ? b == null
                : b != null && string.Compare(a, b, StringComparison.CurrentCultureIgnoreCase) == 0;
        }

        private int TryIncreaseHashcode(int hashcode, string linkField)
        {
            unchecked
            {
                return linkField is null
                    ? hashcode
                    : hashcode * 23 + linkField.ToLower().GetHashCode();
            }
        }
    }
}