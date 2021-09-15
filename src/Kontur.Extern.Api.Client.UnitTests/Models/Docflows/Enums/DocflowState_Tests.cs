using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.UnitTests.Client.Model.TestAssertions;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Models.Docflows.Enums
{
    [TestFixture]
    internal class DocflowState_Tests
    {
        [TestFixture]
        internal class StaticCtor
        {
            [Test]
            public void Should_initialize_namespace_field()
            {
                var ns = DocflowState.Namespace;

                ns.Should().NotBeNull();
            }
        }
        
        [TestFixture]
        internal class Ctor
        {
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            [TestCase("not a urn")]
            public void Should_fail_when_given_invalid_urn(string urn)
            {
                Action action = () => _ = new DocflowState(urn);

                action.Should().Throw<Exception>();
            }

            [Test]
            public void Should_fail_when_given_null_urn()
            {
                Action action = () => _ = new DocflowState((null as Urn)!);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_fail_when_urn_not_belong_to_docflow_type_namespace()
            {
                Action action = () => _ = new DocflowState("urn:document:fss-sedo-error-exchange-error");

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_initialize_with_given_urn()
            {
                var docflowState = new DocflowState("urn:docflow-state:neutral");

                docflowState.ToString().Should().Be("urn:docflow-state:neutral");
            }
        }
        
        [TestFixture]
        internal class PredefinedStates
        {
            [TestCaseSource(nameof(AllPredefinedDocflowStates))]
            public void Should_have_not_empty_values((FieldInfo field, DocflowState? docflowState) predefinedType)
            {
                var (_, docflowState) = predefinedType;
                
                docflowState.Should().NotBeNull();
                docflowState!.Value.ToUrn().Should().NotBeNull();
            }

            [Test]
            public void Should_be_unique() => 
                AllPredefinedDocflowStates.MembersShouldHaveUniqueValues();

            private static IEnumerable<(FieldInfo field, DocflowState? docflowState)> AllPredefinedDocflowStates => 
                EnumLikeType.AllEnumMembersOfStruct<DocflowState>();
        }
    }
}