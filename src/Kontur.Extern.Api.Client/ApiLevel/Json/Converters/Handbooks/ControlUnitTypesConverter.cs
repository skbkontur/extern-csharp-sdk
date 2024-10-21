using System;
using System.Text.Json;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Handbooks;

public class ControlUnitTypesConverter : System.Text.Json.Serialization.JsonConverter<ControlUnitType>
{
    private readonly ILog log;

    public ControlUnitTypesConverter(ILog log) => this.log = log;

    public override ControlUnitType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (Enum.TryParse<ControlUnitType>(value, true, out var controlUnitType))
            return controlUnitType;
        log.Warn("Неизвестное значение ControlUnitType преобразовано в Unknown. Стоит обновить сборку клиента на актуальную");
        return ControlUnitType.Unknown;
    }

    public override void Write(Utf8JsonWriter writer, ControlUnitType value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}