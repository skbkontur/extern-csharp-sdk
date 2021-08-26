#nullable enable
using System;
using System.Linq;
using System.Reflection;
using AutoBogus;

namespace Kontur.Extern.Client.Tests.TestHelpers.BogusExtensions
{
    internal static class AutoFakerExtension
    {
        private static readonly Lazy<MethodInfo> GenerateGenericMethod = new(() =>
        {
            var parameterType = typeof (Action<IAutoGenerateConfigBuilder>);
            var methodInfo = typeof (IAutoFaker).GetMethods()
                .Where(x =>
                {
                    if (x.Name != nameof(IAutoFaker.Generate) || !x.IsGenericMethodDefinition || x.GetGenericArguments().Length != 1)
                        return false;

                    var parameters = x.GetParameters();
                    return parameters.Length == 1 && parameters[0].ParameterType.IsAssignableFrom(parameterType);
                })
                .SingleOrDefault();
            return methodInfo ??
                   throw new InvalidOperationException($"The generic method {nameof(IAutoFaker.Generate)} with parameter {parameterType} not found");
        });

        public static object? Generate(this IAutoFaker faker, Type type, Action<IAutoGenerateConfigBuilder>? configure = null)
        {
            var methodInfo = GenerateGenericMethod.Value;
            return methodInfo
                .MakeGenericMethod(type)
                .Invoke(faker, new object?[] {configure});
        }
    }
}