using System;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.OtherTests
{
    internal class UrnShould
    {
        private Urn urn;
        private Urn urnParent;
        private Urn urnChild;

        [OneTimeSetUp]
        public void SetUp()
        {
            urn = new Urn("1234", "abcd");
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
            // ReSharper disable once NotAccessedVariable
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
            Urn.TryParse("value", out _).Should().BeFalse();
            Urn.TryParse("urn:value", out _).Should().BeTrue();
        }

        [Test]
        public void BeEqual_ToSameUrns()
        {
            var other = new Urn(urn.Nid, urn.Nss);
            urn.Equals(other).Should().BeTrue();
            (urn == other).Should().BeTrue();
            urn.Equals(urn).Should().BeTrue();
        }

        [Test]
        public void BeNotEqual_ToDifferentUrns()
        {
            urn.Equals(urnParent).Should().BeFalse();
            (urn == urnChild).Should().BeFalse();
            urn.Equals(null).Should().BeFalse();
            (urn == default).Should().BeFalse();
        }

        [Test]
        public void CompareCorrectly()
        {
            urnParent.CompareTo(urnChild).Should().BeLessThan(0);
            urn.CompareTo(new Urn("1234", "abce")).Should().BeLessThan(0);
        }

        [Test]
        public void SerializeCorrectly()
        {
            var original = new Urn("docflow", "fns534-ion");
            var serialized = JsonConvert.SerializeObject(original);
            var deserialized = JsonConvert.DeserializeObject<Urn>(serialized);
            deserialized.Should().BeEquivalentTo(original);
        }
    }
}