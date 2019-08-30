using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Docflows.Descriptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kontur.Extern.Client.Models.JsonConverters
{
    internal class DocflowDescriptionConverter : JsonConverter
    {
        private static readonly Dictionary<Type, Func<DocflowDescription>> DocflowGenerator
            = new Dictionary<Type, Func<DocflowDescription>>
            {
                [typeof (ApplicationDescription)] = () => new ApplicationDescription(),
                [typeof (CuLetterDescription)] = () => new CuLetterDescription(),
                [typeof (DemandDescription)] = () => new DemandDescription(),
                [typeof (FssReportDescription)] = () => new FssReportDescription(),
                [typeof (IonDescription)] = () => new IonDescription(),
                [typeof (LetterDescription)] = () => new LetterDescription(),
                [typeof (PfrCuLetterDescription)] = () => new PfrCuLetterDescription(),
                [typeof (PfrIosDescription)] = () => new PfrIosDescription(),
                [typeof (PfrLetterDescription)] = () => new PfrLetterDescription(),
                [typeof (PfrReportDescription)] = () => new PfrReportDescription(),
                [typeof (ReportDescription)] = () => new ReportDescription(),
                [typeof (StatCuLetterDescription)] = () => new StatCuLetterDescription(),
                [typeof (StatLetterDescription)] = () => new StatLetterDescription(),
                [typeof (StatReportDescription)] = () => new StatReportDescription(),
                [typeof (SubmissionDescription)] = () => new SubmissionDescription()
            };

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = (JObject)JToken.ReadFrom(reader);
            var tokens = jObject.Children().ToArray();
            var description = FindCorrectDescription(tokens);
            serializer.Populate(jObject.CreateReader(), description);
            return description;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Implement me if you need");
        }

        public override bool CanConvert(Type objectType) => throw new NotImplementedException("Implement me if you need");

        private static DocflowDescription FindCorrectDescription(JToken[] tokens)
        {
            foreach (var pair in DocflowGenerator)
            {
                var propertyNames = pair.Key.GetProperties().Select(x => x.Name.ToKebabCase()).ToArray();
                if (propertyNames.Length < tokens.Length)
                    continue;
                var correctTypeFound = tokens
                    .Select(token => token.ToObject<JProperty>().Name)
                    .All(tokenName => propertyNames.Contains(tokenName));
                if (correctTypeFound)
                    return pair.Value();
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}