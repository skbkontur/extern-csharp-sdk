using FluentAssertions;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingPolicies;
using Xunit;

namespace Kontur.Extern.Client.Http.UnitTests.Serialization.SysTextJson.NamingPolicies
{
    public class StringCaseConvertExtensions_Tests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData(null, null)]
        [InlineData("URLValue", "url-value")]
        [InlineData("URL", "url")]
        [InlineData("ID", "id")]
        [InlineData("I", "i")]
        [InlineData("Person", "person")]
        [InlineData("iPhone", "i-phone")]
        [InlineData("IPhone", "i-phone")]
        [InlineData("I Phone", "i-phone")]
        [InlineData("I  Phone", "i-phone")]
        [InlineData(" IPhone", "i-phone")]
        [InlineData(" IPhone ", "i-phone")]
        [InlineData("IsCIA", "is-cia")]
        [InlineData("VmQ", "vm-q")]
        [InlineData("Xml2Json", "xml2-json")]
        [InlineData("KeBaBcAsE", "ke-ba-bc-as-e")]
        [InlineData("KeB--aBcAsE", "ke-b--a-bc-as-e")]
        [InlineData("KeB-- aBcAsE", "ke-b--a-bc-as-e")]
        [InlineData("already-kebab-case- ", "already-kebab-case-")]
        [InlineData("IsJSONProperty", "is-json-property")]
        [InlineData("SHOUTING-CASE", "shouting-case")]
        [InlineData("9999-12-31T23:59:59.9999999Z", "9999-12-31-t23:59:59.9999999-z")]
        [InlineData("Hi!! This is text. Time to test.", "hi!!-this-is-text.-time-to-test.")]
        public void ToKebabCase_should_convert_string_to_kebab_case(string value, string expectedValue)
        {
            value.ToKebabCase().Should().Be(expectedValue);
        }
        
        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData(null, null)]
        [InlineData("URLValue", "url_value")]
        [InlineData("URL", "url")]
        [InlineData("ID", "id")]
        [InlineData("I", "i")]
        [InlineData("Person", "person")]
        [InlineData("iPhone", "i_phone")]
        [InlineData("IPhone", "i_phone")]
        [InlineData("I Phone", "i_phone")]
        [InlineData("I  Phone", "i_phone")]
        [InlineData(" IPhone", "i_phone")]
        [InlineData(" IPhone ", "i_phone")]
        [InlineData("IsCIA", "is_cia")]
        [InlineData("VmQ", "vm_q")]
        [InlineData("Xml2Json", "xml2_json")]
        [InlineData("SnaKeCaSe", "sna_ke_ca_se")]
        [InlineData("SnaK__eCaSe", "sna_k__e_ca_se")]
        [InlineData("SnaK__  eCaSe", "sna_k__e_ca_se")]
        [InlineData("SnaKe--CaSe", "sna_ke--_ca_se")]
        [InlineData("already_snake_case_ ", "already_snake_case_")]
        [InlineData("IsJSONProperty", "is_json_property")]
        [InlineData("SHOUTING_CASE", "shouting_case")]
        [InlineData("9999_12_31T23:59:59.9999999Z", "9999_12_31_t23:59:59.9999999_z")]
        [InlineData("Hi!! This is text. Time to test.", "hi!!_this_is_text._time_to_test.")]
        public void ToSnakeCase_should_convert_string_to_snake_case(string value, string expectedValue)
        {
            value.ToSnakeCase().Should().Be(expectedValue);
        }
    }
}