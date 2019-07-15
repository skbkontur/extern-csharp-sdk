using System;
using System.Text;

namespace ExternDotnetSDK.Common
{
    /// <summary>
    ///     Просто ссылка.
    ///     Подробности на https://wiki.skbkontur.ru/pages/viewpage.action?pageId=82510147.
    /// </summary>
    public sealed class Link : IEquatable<Link>
    {
        public const string RelSelf = "self";
        public const string RelPrev = "prev";
        public const string RelNext = "next";

        public Link(Uri href, string rel, string name = null, string title = null, string profile = null, bool templated = false)
        {
            if (href == null)
                throw new ArgumentNullException(nameof(href));

            if (rel == null)
                throw new ArgumentNullException(nameof(rel));

            this.Href = href;
            this.Rel = rel;
            this.Name = name;
            this.Title = title;
            this.Templated = templated;
            this.Profile = profile;
        }

        /// <summary>
        ///     Ссылка на ресурс.
        /// </summary>
        public Uri Href { get; private set; }

        /// <summary>
        ///     Тип отношения.
        /// </summary>
        public string Rel { get; private set; }

        /// <summary>
        ///     Имя отношения.
        ///     Используется для идентификации ресурсов с одинаковым типом отношения.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Человек-понятное имя ресурса.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        ///     Профиль представления.
        /// </summary>
        public string Profile { get; private set; }

        /// <summary>
        ///     Определяет шаблонную ссылку.
        /// </summary>
        public bool Templated { get; private set; }

        public bool Equals(Link other)
        {
            return other != null &&
                   Href == other.Href &&
                   EqualsOrNulls(Rel, other.Rel) &&
                   EqualsOrNulls(Title, other.Title) &&
                   EqualsOrNulls(Name, other.Name) &&
                   EqualsOrNulls(Profile, other.Profile) &&
                   Templated == other.Templated;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("<link ");

            if (Rel != null)
                builder.Append("rel=\"").Append(Rel).Append("\" ");

            if (Name != null)
                builder.Append("name=\"").Append(Name).Append("\" ");

            if (Profile != null)
                builder.Append("profile=\"").Append(Profile).Append("\" ");

            if (Title != null)
                builder.Append("title=\"").Append(Title).Append("\" ");

            if (Templated)
                builder.Append("templated=\"true\" ");

            builder.Append("href=\"").Append(Href).Append("\" />");

            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Link);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashcode = 19;

                hashcode = hashcode*23 + Href.GetHashCode();

                if (Title != null)
                    hashcode = hashcode*23 + Title.ToLower().GetHashCode();

                if (Rel != null)
                    hashcode = hashcode*23 + Rel.ToLower().GetHashCode();

                if (Name != null)
                    hashcode = hashcode*23 + Name.ToLower().GetHashCode();

                if (Profile != null)
                    hashcode = hashcode*23 + Profile.ToLower().GetHashCode();

                hashcode = hashcode*23 + Templated.GetHashCode();

                return hashcode;
            }
        }

        private static bool EqualsOrNulls(string a, string b)
        {
            if (a == null)
                return b == null;

            if (b == null)
                return false;

            return 0 == string.Compare(a, b, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}