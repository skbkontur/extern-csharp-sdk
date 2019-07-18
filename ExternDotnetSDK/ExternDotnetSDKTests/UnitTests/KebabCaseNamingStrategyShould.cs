using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternDotnetSDK.Common;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ExternDotnetSDKTests.UnitTests
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    internal class TestClass
    {
        public int field;
        public int longField;
        public int VeryLongField;
    }

    [TestFixture]
    class KebabCaseNamingStrategyShould
    {
        [Test]
        public void SerializeClassesCorrectly()
        {
            var serialized = JsonConvert.SerializeObject(new TestClass());
            Assert.False(serialized.Any(char.IsUpper));
            serialized.Should().Contain("-");
        }
    }
}