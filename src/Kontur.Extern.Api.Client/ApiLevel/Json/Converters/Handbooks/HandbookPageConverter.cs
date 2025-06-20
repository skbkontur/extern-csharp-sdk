using System;
using System.Collections.Generic;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.NamingPolicies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Handbooks;

public class HandbookPageConverter : JsonConverter<HandbookPage>
{
    private readonly Dictionary<HandbookType, Func<HandbookItem>> handbookToType = new() 
    {
        {HandbookType.Okved, () => new Okved()},
        {HandbookType.MvdCitizenship, () => new MvdCitizenship()},
        {HandbookType.MvdEmployerStatus, () => new EmployerStatus()},
        {HandbookType.MvdWorkPermits, () => new MvdWorkPermits()},
        {HandbookType.MvdProfessionsSpeciality,() => new MvdProfessionsSpeciality()},
        {HandbookType.MvdHqsEmployerStatus, () => new EmployerStatusHqs()},
        {HandbookType.MvdRegionsRf, () => new MvdRfRegions()},
    };

    public override void WriteJson(JsonWriter writer, HandbookPage value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override HandbookPage ReadJson(JsonReader reader, Type objectType, HandbookPage existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jObject = (JObject)JToken.ReadFrom(reader);
        var enumVal = jObject["handbook-type"].ToString().KebabToCamelCase();
        var type = (HandbookType)Enum.Parse(typeof(HandbookType), enumVal, true);

        if (!handbookToType.ContainsKey(type))
            return null;

        var jArray = JArray.Load(jObject["handbook"].CreateReader());
        var handbookList = new List<HandbookItem>();
        foreach (var token in jArray)
        {
            var item = handbookToType[type]();
            serializer.Populate(token.CreateReader(), item);
            handbookList.Add(item);
        }
        jObject.Remove("handbook");

        var result = new HandbookPage();
        serializer.Populate(jObject.CreateReader(), result);
        result.Handbook = handbookList.ToArray();

        return result;
    }

    public override bool CanWrite => false;
}