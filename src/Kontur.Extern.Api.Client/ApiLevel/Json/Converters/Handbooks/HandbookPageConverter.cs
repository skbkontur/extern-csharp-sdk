using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.NamingPolicies;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Handbooks;

public class HandbookPageConverter : JsonConverter<HandbookPage>
{
    private readonly Dictionary<HandbookType, Type> handbookToType = new() 
    {
        {HandbookType.Okved, typeof(Okved)},
        {HandbookType.MvdCitizenship, typeof(MvdCitizenship)},
        {HandbookType.MvdEmployerStatus, typeof(EmployerStatus)},
        {HandbookType.MvdWorkPermits, typeof(MvdWorkPermits)},
        {HandbookType.MvdProfessionsSpeciality,typeof(MvdProfessionsSpeciality)},
        {HandbookType.MvdHqsEmployerStatus, typeof(EmployerStatusHqs)},
        {HandbookType.MvdRegionsRf, typeof(MvdRfRegions)},
    };

    public override HandbookPage? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;

        var enumVal = root.GetProperty("handbook-type").GetString()!.KebabToCamelCase();
        var handbookType = Enum.TryParse<HandbookType>(enumVal, true, out var parsedType) 
            ? parsedType 
            : throw Errors.UnexpectedEnumMember(nameof(HandbookType), parsedType);

        var itemType = handbookToType[handbookType];
        var items = root.GetProperty("handbook")
            .EnumerateArray()
            .Select(e => (HandbookItem)JsonSerializer.Deserialize(e.GetRawText(), itemType, options))
            .ToArray();

        var handbookPage = new HandbookPage
        {
            Skip = root.GetProperty("skip").GetInt32(),
            Take = root.GetProperty("take").GetInt32(),
            TotalCount = root.GetProperty("total-count").GetInt32(),
            HandbookType = handbookType,
            Handbook = items
        };

        return handbookPage;
    }

    public override void Write(Utf8JsonWriter writer, HandbookPage value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}