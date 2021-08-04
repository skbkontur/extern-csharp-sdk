using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Kontur.Extern.Client.Model.Documents;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Client.Model.Documents
{
    [TestFixture]
    public class DocumentType_Tests
    {
        [TestFixture]
        internal class Ctor
        {
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            [TestCase("not a urn")]
            public void Should_fail_when_given_invalid_urn(string urn)
            {
                Action action = () => _ = new DocumentType(urn);

                action.Should().Throw<Exception>();
            }

            [Test]
            public void Should_initialize_with_given_urn()
            {
                var documentType = new DocumentType("urn:document:fss-sedo-error-exchange-error");

                documentType.ToString().Should().Be("urn:document:fss-sedo-error-exchange-error");
            }
        }

        [TestFixture]
        internal class PredefinedTypes
        {
            [TestCaseSource(nameof(PredefinedTypeFields))]
            public void Should_specify_all_predefined_fields_values(FieldInfo predefinedTypeField)
            {
                var documentType = (DocumentType) predefinedTypeField.GetValue(null!)!;

                documentType.ToUrn().Should().NotBeNull();
            }

            public static IEnumerable<FieldInfo> PredefinedTypeFields() =>
                from nestedType in GetNestedTypes() 
                from fieldInfo in nestedType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField) 
                where fieldInfo.FieldType == typeof (DocumentType) 
                select fieldInfo;

            private static IEnumerable<Type> GetNestedTypes()
            {
                var stack = new Stack<Type>();
                stack.Push(typeof (DocumentType));

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
}