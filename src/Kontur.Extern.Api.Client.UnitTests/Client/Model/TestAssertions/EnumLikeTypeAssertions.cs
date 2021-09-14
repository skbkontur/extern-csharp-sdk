using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.TestAssertions
{
    public static class EnumLikeTypeAssertions
    {
        public static void MembersShouldHaveUniqueValues<T>(this IEnumerable<(FieldInfo field, T? value)> members) => 
            MembersShouldHaveUniqueValuesExcept(members);
        
        public static void MembersShouldHaveUniqueValuesExcept<T>(this IEnumerable<(FieldInfo field, T? value)> members, params T[] ignoredValues)
        {
            var values = members
                .Where(x => x.value is null || !ignoredValues.Contains(x.value))
                .Select(x => x.value?.ToString() ?? "<null>");

            var valuesFrequencies = values
                .GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count())
                .As<IEnumerable<KeyValuePair<string, int>>>();

            valuesFrequencies.Should().OnlyContain(x => x.Value == 1);
        }
    }
}