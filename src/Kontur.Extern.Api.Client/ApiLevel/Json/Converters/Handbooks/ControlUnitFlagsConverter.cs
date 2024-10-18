using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Handbooks;

internal class ControlUnitFlagsConverter : JsonConverter<ControlUnitFlags>
{
    private readonly ILog log;

    public ControlUnitFlagsConverter(ILog log) => this.log = log;

    public override ControlUnitFlags Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = ConvertToPascalCase(reader.GetString());
        if (Enum.TryParse<ControlUnitFlags>(value, true, out var controlUnitFlag))
            return controlUnitFlag;
        log.Warn("Необходимо обновить модель ControlUnitFlags");
        return ControlUnitFlags.Unknown;
    }

    public override void Write(Utf8JsonWriter writer, ControlUnitFlags value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());

    private static string ConvertToPascalCase(string input)
    {
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(input).Replace("-", "");
    }
}