using System;
using AutoBogus;
using Bogus;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Tests.TestHelpers.BogusExtensions
{
    internal static class AutoGenerateConfigBuilderExtension
    {
        [PublicAPI]
        public static IAutoGenerateConfigBuilder RuleForPropNameOf<TValue>(this IAutoGenerateConfigBuilder builder, 
                                                                           string propName,
                                                                           Func<Faker, TValue> generator,
                                                                           StringComparison nameComparison = StringComparison.OrdinalIgnoreCase)
        {
            return builder.WithOverride(new PropNameGenerator<TValue>(propName, nameComparison, generator));
        }
        
        private class PropNameGenerator<TValue> : AutoGeneratorOverride
        {
            private readonly string propName;
            private readonly StringComparison nameComparison;
            private readonly Func<Faker, TValue> generator;

            public PropNameGenerator(string propName, StringComparison nameComparison, Func<Faker, TValue> generator)
            {
                this.propName = propName;
                this.nameComparison = nameComparison;
                this.generator = generator;
            }

            public override bool CanOverride(AutoGenerateContext context) =>
                context.GenerateName is not null && 
                context.GenerateName.Equals(propName, nameComparison) && 
                context.GenerateType.IsAssignableTo(typeof (TValue));

            public override void Generate(AutoGenerateOverrideContext context) => 
                context.Instance = generator(context.Faker);
        }
    }
}