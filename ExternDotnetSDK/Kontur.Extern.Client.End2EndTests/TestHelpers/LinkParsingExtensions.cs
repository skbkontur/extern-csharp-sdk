using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Kontur.Extern.Client.Models.Common;

namespace Kontur.Extern.Client.End2EndTests.TestHelpers
{
    internal static class LinkParsingExtensions
    {
        private static readonly Regex GuidRegex = new Regex(
            @"(\{?[a-zA-Z0-9]{8}(-{0,1})[a-zA-Z0-9]{4}(-{0,1})[a-zA-Z0-9]{4}(-{0,1})[a-zA-Z0-9]{4}(-{0,1})[a-zA-Z0-9]{12}\}?)",
            RegexOptions.Compiled | RegexOptions.Singleline
        );

        public static IEnumerable<Guid> ExtractGuids(this Link link)
        {
            var matches = GuidRegex.Matches(link.Href.AbsolutePath);
            foreach (Match match in matches)
            {
                yield return Guid.Parse(match.Value);
            }
        }
    }
}