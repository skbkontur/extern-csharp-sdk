using Newtonsoft.Json;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks.JsonNetAdapters
{
    internal static class JsonSerializerExtension
    {
        public static JsonSerializer RemoveConverter(this JsonSerializer serializer, JsonConverter converter)
        {
            var clone = serializer.Clone();
            clone.Converters.Remove(converter);
            return clone;
        }

        private static JsonSerializer Clone(this JsonSerializer serializer)
        {
            var clone = new JsonSerializer
            {
                Culture = serializer.Culture,
                Formatting = serializer.Formatting,
                ConstructorHandling = serializer.ConstructorHandling,
                EqualityComparer = serializer.EqualityComparer,
                ContractResolver = serializer.ContractResolver,
                MaxDepth = serializer.MaxDepth,
                ReferenceResolver = serializer.ReferenceResolver,
                SerializationBinder = serializer.SerializationBinder,
                TraceWriter = serializer.TraceWriter,
                CheckAdditionalContent = serializer.CheckAdditionalContent,
                DateFormatHandling = serializer.DateFormatHandling,
                DateFormatString = serializer.DateFormatString,
                DateParseHandling = serializer.DateParseHandling,
                DefaultValueHandling = serializer.DefaultValueHandling,
                FloatFormatHandling = serializer.FloatFormatHandling,
                FloatParseHandling = serializer.FloatParseHandling,
                MetadataPropertyHandling = serializer.MetadataPropertyHandling,
                MissingMemberHandling = serializer.MissingMemberHandling,
                NullValueHandling = serializer.NullValueHandling,
                ObjectCreationHandling = serializer.ObjectCreationHandling,
                PreserveReferencesHandling = serializer.PreserveReferencesHandling,
                ReferenceLoopHandling = serializer.ReferenceLoopHandling,
                StringEscapeHandling = serializer.StringEscapeHandling,
                TypeNameHandling = serializer.TypeNameHandling,
                DateTimeZoneHandling = serializer.DateTimeZoneHandling,
                TypeNameAssemblyFormatHandling = serializer.TypeNameAssemblyFormatHandling,
                Context = serializer.Context
            };
            foreach (var converter in serializer.Converters)
            {
                clone.Converters.Add(converter);
            }
            return clone;
        }
    }
}