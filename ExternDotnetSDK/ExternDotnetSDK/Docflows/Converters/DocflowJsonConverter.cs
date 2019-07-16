//using System;
//using System.Collections.Generic;
//using System.IO;
//using ExternDotnetSDK.Common;
//using ExternDotnetSDK.Docflows.Descriptions;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//namespace ExternDotnetSDK.Docflows
//{
//    public class DocflowJsonConverter : JsonConverter
//    {
//        public static Urn DocflowTypesFns534InventoryOnRelatedDocflow { get; } = new Urn("urn:docflow:fns534-inventory");

//        private static readonly Dictionary<Urn, Func<DocflowDescription>> DocflowGenerator = new Dictionary<Urn, Func<DocflowDescription>>
//        {
//            [DocflowTypes.Fns534Report] = () => new ReportDescription(),
//            [DocflowTypes.Fns534Demand] = () => new DemandDescription(),
//            [DocflowTypes.Fns534Submission] = () => new SubmissionDescription(),
//            [DocflowTypesFns534InventoryOnRelatedDocflow] = () => new SubmissionDescription(), //special dc-type
//            [DocflowTypes.Fns534Ion] = () => new IonDescription(),
//            [DocflowTypes.Fns534Letter] = () => new LetterDescription(),
//            [DocflowTypes.Fns534CuLetter] = () => new LetterDescription(),
//            [DocflowTypes.Fns534Application] = () => new ApplicationDescription(),
//            [DocflowTypes.StatReport] = () => new StatReportDescription(),
//            [DocflowTypes.StatLetter] = () => new StatLetterDescription(),
//            [DocflowTypes.StatCuLetter] = () => new StatCuLetterDescription(),
//            [DocflowTypes.PfrReport] = () => new PfrReportDescription(),
//            [DocflowTypes.PfrIos] = () => new PfrIosDescription(),
//            [DocflowTypes.PfrLetter] = () => new PfrLetterDescription(),
//            [DocflowTypes.PfrCuLetter] = () => new PfrCuLetterDescription(),
//            [DocflowTypes.FssReport] = () => new FssReportDescription(),
//            [DocflowTypes.FssSickReport] = () => new FssSickReportDescription(),
//            [BizRegDocflowTypes.Registration] = () => new BusinessRegistrationDescription(),
//        };
//        public override bool CanWrite => false;

//        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//        {
//            var jObject = (JObject) JToken.ReadFrom(reader);
//            var type = new Urn((string) jObject["type"]);

//            if (!DocflowGenerator.ContainsKey(type))
//                throw new ArgumentOutOfRangeException();

//            var description = DocflowGenerator[type]();
//            serializer.Populate(new JsonTextReader(new StringReader(jObject["description"].ToString())), description);
//            jObject.Remove("description");

//            var result = new Docflow();
//            serializer.Populate(jObject.CreateReader(), result);
//            result.Description = description;

//            return result;
//        }

//        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//        {
//            throw new NotImplementedException();
//        }

//        public override bool CanConvert(Type objectType)
//        {
//            return typeof (Docflow).IsAssignableFrom(objectType);
//        }
//    }
//}
