using System;
using ExternDotnetSDK.Common;
using FluentAssertions;
using NUnit.Framework;

namespace ExternDotnetSDKTests.UnitTests
{
    internal class UrnShould
    {
        private Urn urn;
        private Urn urnParent;
        private Urn urnChild;

        [OneTimeSetUp]
        public void SetUp()
        {
            urn = new Urn("1234","abcd");
            urnParent = new Urn("myNid", "myNss");
            urnChild = new Urn("myNid:myNss", "child");
        }

        [Test]
        public void HaveCorrectParentalMethods()
        {
            urnParent.IsParentOf(urnChild).Should().BeTrue();
            urnParent.IsChildOf(urnChild).Should().BeFalse();
            urn.IsParentOf(urnParent).Should().BeFalse();
            urn.IsChildOf(urnParent).Should().BeFalse();
            urn.CreateChild("child").IsChildOf(urn).Should().BeTrue();
        }

        [Test]
        public void Throw_WhenConstructedWithBadParameters()
        {
            Urn badUrn;
            Assert.Throws<UrnException>(() => badUrn = new Urn("bad value"), "Invalid URN schema");
            Assert.Throws<ArgumentNullException>(() => badUrn = new Urn(null));
            Assert.Throws<ArgumentNullException>(() => badUrn = new Urn(string.Empty, "nss"));
            Assert.Throws<ArgumentNullException>(() => badUrn = new Urn("nid", string.Empty));
            Assert.Throws<ArgumentNullException>(() => badUrn = new Urn(urnParent, null));
            Assert.Throws<ArgumentNullException>(() => badUrn = new Urn(default(Urn), "nss"));
        }

        [Test]
        public void TryParseCorrectly()
        {
            Urn result;
            Urn.TryParse("value", out result).Should().BeFalse();
            Urn.TryParse("urn:value", out result).Should().BeTrue();
        }

        [Test]
        public void BeEqual_ToSameUrns()
        {
            urn.Equals(new Urn(urn.Nid, urn.Nss)).Should().BeTrue();
            urn.Equals(urn).Should().BeTrue();
        }

        [Test]
        public void BeNotEqual_ToDifferentUrns()
        {
            urn.Equals(urnParent).Should().BeFalse();
            (urn == urnChild).Should().BeFalse();
            urn.Equals(null).Should().BeFalse();
            (urn == default(Urn)).Should().BeFalse();
        }

        [Test]
        public void CompareCorrectly()
        {
            urnParent.CompareTo(urnChild).Should().BeLessThan(0);
            urn.CompareTo(new Urn("1234", "abce")).Should().BeLessThan(0);
        }
    }
}