using System;
using System.Collections.Generic;

namespace Kontur.Extern.Client.Testing.Helpers
{
    public static class TypeExtension
    {
        public static IEnumerable<Type> EnumerateAllNestedTypes(this Type targetType)
        {
            var stack = new Stack<Type>();
            stack.Push(targetType);

            while (stack.TryPop(out var type))
            {
                foreach (var nestedType in type.GetNestedTypes())
                {
                    yield return nestedType;
                    stack.Push(nestedType);
                }
            }
        }
    }
}