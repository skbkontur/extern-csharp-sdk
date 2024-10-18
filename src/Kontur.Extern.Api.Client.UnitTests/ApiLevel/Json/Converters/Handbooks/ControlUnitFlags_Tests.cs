using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.Http.Serialization;
using NUnit.Framework;
using Vostok.Logging.Console;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.Handbooks;

[TestFixture]
public class ControlUnitFlags_Tests
{
    private static IJsonSerializer serializer = null!;

    [SetUp]
    public void SetUp() => serializer = JsonSerializerFactory.CreateJsonSerializer(new ConsoleLog());

    [TestCase("business-registration", ExpectedResult = ControlUnitFlags.BusinessRegistration)]
    [TestCase("is-active", ExpectedResult = ControlUnitFlags.IsActive)]
    [TestCase("is-test", ExpectedResult = ControlUnitFlags.IsTest)]
    [TestCase("hello", ExpectedResult = ControlUnitFlags.Unknown)]
    public ControlUnitFlags Should_return_null_description_if_it_is_known_but_missed(string value)
    {
        return serializer.Deserialize<ControlUnitFlags>(serializer.SerializeToIndentedString(value));
    }
}