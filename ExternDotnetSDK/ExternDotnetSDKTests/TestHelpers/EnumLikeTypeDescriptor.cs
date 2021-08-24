#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kontur.Extern.Client.Model.Documents;
using Kontur.Extern.Client.Testing.Helpers;

namespace Kontur.Extern.Client.Tests.TestHelpers
{
    internal static class EnumLikeType
    {
        public static IEnumerable<T> AllEnumValuesFromNestedTypesOfStruct<T>()
            where T : struct
        {
            return AllEnumMembersFromNestedTypesOfStruct<T>()
                .Select(x => x.value)
                .Where(x => x.HasValue)
                .Select(x => x!.Value);
        }
        
        public static IEnumerable<(FieldInfo field, T? value)> AllEnumMembersFromNestedTypesOfStruct<T>()
            where T : struct
        {
            return EnumMembersFromNestedTypesOf<T>().Select(x => (x, (T?)x.GetValue(null!)));
        }

        private static IEnumerable<FieldInfo> EnumMembersFromNestedTypesOf<T>()
        {
            var fields = (
                from nestedType in typeof (T).EnumerateAllNestedTypes()
                from fieldInfo in nestedType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField)
                where fieldInfo.FieldType == typeof (T)
                select fieldInfo
            ).ToArray();

            if (fields.Length == 0)
                throw new InvalidOperationException($"Type {typeof(DocumentType)} does not contain predefined document types");
                
            return fields;
        }
    }
}