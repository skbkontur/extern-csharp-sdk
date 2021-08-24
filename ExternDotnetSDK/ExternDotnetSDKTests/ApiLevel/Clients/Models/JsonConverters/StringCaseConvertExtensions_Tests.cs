using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.JsonConverters
{
    internal class StringCaseConvertExtensions_Tests
    {
        [TestCase("", "")]
        [TestCase(" ", "")]
        [TestCase(null, null)]
        [TestCase("URLValue", "url-value")]
        [TestCase("URL", "url")]
        [TestCase("ID", "id")]
        [TestCase("I", "i")]
        [TestCase("Person", "person")]
        [TestCase("iPhone", "i-phone")]
        [TestCase("IPhone", "i-phone")]
        [TestCase("I Phone", "i-phone")]
        [TestCase("I  Phone", "i-phone")]
        [TestCase(" IPhone", "i-phone")]
        [TestCase(" IPhone ", "i-phone")]
        [TestCase("IsCIA", "is-cia")]
        [TestCase("VmQ", "vm-q")]
        [TestCase("Xml2Json", "xml2-json")]
        [TestCase("KeBaBcAsE", "ke-ba-bc-as-e")]
        [TestCase("KeB--aBcAsE", "ke-b--a-bc-as-e")]
        [TestCase("KeB-- aBcAsE", "ke-b--a-bc-as-e")]
        [TestCase("already-kebab-case- ", "already-kebab-case-")]
        [TestCase("IsJSONProperty", "is-json-property")]
        [TestCase("SHOUTING-CASE", "shouting-case")]
        [TestCase("9999-12-31T23:59:59.9999999Z", "9999-12-31-t23:59:59.9999999-z")]
        [TestCase("Hi!! This is text. Time to test.", "hi!!-this-is-text.-time-to-test.")]
        public void ToKebabCase_should_convert_string_to_kebab_case(string value, string expectedValue)
        {
            value.ToKebabCase().Should().Be(expectedValue);
        }
    }
}