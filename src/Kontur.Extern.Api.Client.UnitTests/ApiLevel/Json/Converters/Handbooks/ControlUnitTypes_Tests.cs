using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.Http.Serialization;
using NUnit.Framework;
using Vostok.Logging.Console;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.Handbooks;

public class ControlUnitTypes_Tests
{
    private static IJsonSerializer serializer = null!;

    [SetUp]
    public void SetUp() => serializer = JsonSerializerFactory.CreateJsonSerializer(new ConsoleLog());

    [TestCase("fns", ExpectedResult = ControlUnitType.Fns)]
    [TestCase("fss", ExpectedResult = ControlUnitType.Fss)]
    [TestCase("fst", ExpectedResult = ControlUnitType.Fst)]
    [TestCase("pfr", ExpectedResult = ControlUnitType.Pfr)]
    [TestCase("rtn", ExpectedResult = ControlUnitType.Rtn)]
    [TestCase("stat", ExpectedResult = ControlUnitType.Stat)]
    [TestCase("hello", ExpectedResult = ControlUnitType.Unknown)]
    public ControlUnitType Should_return_null_description_if_it_is_known_but_missed(string value)
    {
        return serializer.Deserialize<ControlUnitType>(serializer.SerializeToIndentedString(value));
    }
}