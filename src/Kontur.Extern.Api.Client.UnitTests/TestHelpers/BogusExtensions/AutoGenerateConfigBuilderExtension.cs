using System;
using AutoBogus;
using Bogus;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.UnitTests.TestHelpers.BogusExtensions
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
        
        [PublicAPI]
        public static IAutoGenerateConfigBuilder RuleForType<TValue>(this IAutoGenerateConfigBuilder builder, Func<Faker, TValue> generator) => 
            builder.WithOverride(new TypeGenerator<TValue>(generator));

        private class TypeGenerator<TValue> : AutoGeneratorOverride
        {
            private readonly Func<Faker, TValue> generator;

            public TypeGenerator(Func<Faker, TValue> generator) => this.generator = generator;

            public override bool Preinitialize => false;

            public override bool CanOverride(AutoGenerateContext context) =>
                context.GenerateType.IsAssignableTo(typeof (TValue));

            public override void Generate(AutoGenerateOverrideContext context) => 
                context.Instance = generator(context.Faker);
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