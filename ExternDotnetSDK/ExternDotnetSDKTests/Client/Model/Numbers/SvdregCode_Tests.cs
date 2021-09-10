using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Kontur.Extern.Client.Model.Numbers.BusinessRegistration;
using Kontur.Extern.Client.Tests.Client.Model.TestAssertions;
using Kontur.Extern.Client.Tests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Client.Model.Numbers
{
    [TestFixture]
    internal class SvdregCode_Tests
    {
        [TestFixture]
        internal class Parser
        {
            [TestCaseSource(nameof(InvalidStrings))]
            public void Should_fail_when_the_given_number_string_is_invalid(string value)
            {
                Action action = () => SvdregCode.Parser.Parse(value);

                action.Should().Throw<ArgumentException>();
            }

            [TestCaseSource(nameof(ValidStrings))]
            public void Should_successfully_return_a_code_when_the_given_value_is_valid(string value)
            {
                var parsedValue = SvdregCode.Parser.Parse(value);

                parsedValue.Code.Should().Be(value);
            }
            
            private static IEnumerable<string> InvalidStrings
            {
                get
                {
                    yield return "12345";
                    yield return " 012345";
                    yield return "012345 ";
                    yield return "012-345";
                    yield return "Y12345";
                    yield return "x12345";
                    yield return "z12345";
                    yield return "01234X";
                }
            }
        
            private static IEnumerable<string> ValidStrings
            {
                get
                {
                    yield return "012345";
                    yield return "010011";
                    yield return "X12345";
                }
            }
        }

        [TestFixture]
        internal class PredefinedTypes
        {
            [TestCaseSource(nameof(AllPredefinedSvdRegCodes))]
            public void Should_have_not_empty_values((FieldInfo field, SvdregCode? code) predefinedType)
            {
                var (_, code) = predefinedType;
                
                code.Should().NotBeNull();
                code!.Value.Code.Should().NotBeNull();
            }

            [Test]
            public void Should_be_unique() => 
                AllPredefinedSvdRegCodes.MembersShouldHaveUniqueValues();

            private static IEnumerable<(FieldInfo field, SvdregCode? code)> AllPredefinedSvdRegCodes => 
                EnumLikeType.AllEnumMembersFromNestedTypesOfStruct<SvdregCode>();
        }
    }
}