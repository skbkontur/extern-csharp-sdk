using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kontur.Extern.Api.Client.Testing.Helpers;

namespace Kontur.Extern.Api.Client.UnitTests.TestHelpers
{
    internal static class EnumLikeType
    {
        public static IEnumerable<T> AllEnumValuesOfStruct<T>()
            where T : struct
        {
            return AllEnumMembersOfStruct<T>()
                .Select(x => x.value)
                .Where(x => x.HasValue)
                .Select(x => x!.Value);
        }
        
        public static IEnumerable<(FieldInfo field, T? value)> AllEnumMembersOfStruct<T>()
            where T : struct
        {
            var fields = (
                from fieldInfo in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField)
                where fieldInfo.FieldType == typeof (T)
                select fieldInfo
            ).ToArray();

            if (fields.Length == 0)
                throw new InvalidOperationException($"Type {typeof(T)} does not contain predefined values of {typeof(T)}");
            return fields.Select(x => (x, (T?) x.GetValue(null!)));
        }
        
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
            return AllEnumMembersFromNestedTypesOfStruct<T, T>();
        }
        
        public static IEnumerable<(FieldInfo field, TValue? value)> AllEnumMembersFromNestedTypesOfStruct<T, TValue>()
            where TValue : struct
        {
            return EnumMembersFromNestedTypesOf<T, TValue>().Select(x => (x, (TValue?) x.GetValue(null!)));
        }

        public static IEnumerable<(FieldInfo field, TValue? value)> AllEnumMembersFromNestedTypes<T, TValue>() => 
            EnumMembersFromNestedTypesOf<T, TValue>().Select(x => (x, (TValue?) x.GetValue(null!)));

        private static IEnumerable<FieldInfo> EnumMembersFromNestedTypesOf<T, TValue>()
        {
            var fields = (
                from nestedType in typeof (T).EnumerateAllNestedTypes()
                from fieldInfo in nestedType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField)
                where fieldInfo.FieldType == typeof (TValue)
                select fieldInfo
            ).ToArray();

            if (fields.Length == 0)
                throw new InvalidOperationException($"Type {typeof(T)} does not contain predefined values of {typeof(TValue)}");
                
            return fields;
        }
    }
}