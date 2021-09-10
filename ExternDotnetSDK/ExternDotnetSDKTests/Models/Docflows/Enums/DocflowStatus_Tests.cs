using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows.Enums;
using Kontur.Extern.Client.Tests.Client.Model.TestAssertions;
using Kontur.Extern.Client.Tests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Models.Docflows.Enums
{
    [TestFixture]
    internal class DocflowStatus_Tests
    {
        [TestFixture]
        internal class StaticCtor
        {
            [Test]
            public void Should_initialize_namespace_field()
            {
                var ns = DocflowStatus.Namespace;

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
                Action action = () => _ = new DocflowStatus(urn);

                action.Should().Throw<Exception>();
            }

            [Test]
            public void Should_fail_when_given_null_urn()
            {
                Action action = () => _ = new DocflowStatus((null as Urn)!);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_fail_when_urn_not_belong_to_docflow_type_namespace()
            {
                Action action = () => _ = new DocflowStatus("urn:document:fss-sedo-error-exchange-error");

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_initialize_with_given_urn()
            {
                var docflowStatus = new DocflowStatus("urn:docflow-common-status:sent");

                docflowStatus.ToString().Should().Be("urn:docflow-common-status:sent");
            }
        }
        
        [TestFixture]
        internal class PredefinedStates
        {
            [TestCaseSource(nameof(AllPredefinedDocflowStatuses))]
            public void Should_have_not_empty_values((FieldInfo field, DocflowStatus? docflowStatus) predefinedType)
            {
                var (_, docflowStatus) = predefinedType;
                
                docflowStatus.Should().NotBeNull();
                docflowStatus!.Value.ToUrn().Should().NotBeNull();
            }

            [Test]
            public void Should_be_unique() => 
                AllPredefinedDocflowStatuses.MembersShouldHaveUniqueValues();

            private static IEnumerable<(FieldInfo field, DocflowStatus? status)> AllPredefinedDocflowStatuses => 
                EnumLikeType.AllEnumMembersOfStruct<DocflowStatus>();
        }
    }
}