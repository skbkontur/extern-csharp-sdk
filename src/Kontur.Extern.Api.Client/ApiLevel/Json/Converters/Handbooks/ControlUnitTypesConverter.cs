using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Handbooks;

internal class ControlUnitTypesConverter : JsonConverter<ControlUnitType?>
{
    private readonly ILog log;
    public override bool HandleNull => true;

    public ControlUnitTypesConverter(ILog log) => this.log = log;

    public override ControlUnitType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (Enum.TryParse<ControlUnitType>(value, true, out var controlUnitType))
            return controlUnitType;
        log.Warn("Неизвестное значение ControlUnitType преобразовано в null. Обновите сборку клиента на актуальную");
        return null;
    }

    public override void Write(Utf8JsonWriter writer, ControlUnitType? value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}