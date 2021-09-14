using System;
using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Models.DraftsBuilders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.JsonConverters.DraftBuilders
{
    internal class DraftsBuilderMetasSerializationTester<TMeta, TData>
        where TMeta : class, IDraftsBuilderMeta<TData>
        where TData : class
    {
        private readonly IJsonSerializer serializer;
        private readonly DraftsBuilderMetasGenerator<TMeta, TData> metaGenerator;

        public DraftsBuilderMetasSerializationTester(Func<TData> unknownDataFactory)
        {
            serializer = JsonSerializerFactory.CreateJsonSerializer(ignoreNullValues: false);
            metaGenerator = new DraftsBuilderMetasGenerator<TMeta, TData>(unknownDataFactory);
        }
        
        public (string json, TMeta meta) GenerateWithData(Type? builderDataType, DraftBuilderType builderType)
        {
            var builderMeta = metaGenerator.GenerateWithData(builderDataType, builderType);
            var json = serializer.SerializeToIndentedString(builderMeta);
            Console.WriteLine($"Generated JSON: {json}");
            return (json, builderMeta);
        }
        
        public (string json, TMeta meta) GenerateUnknownBuilder(TData? data)
        {
            var unknownBuilderType = new DraftBuilderType(DraftBuilderType.Namespace.Append("unknown-builder"));
            var meta = metaGenerator.GenerateWithoutData(unknownBuilderType);
            meta.BuilderData = data!;
            var json = serializer.SerializeToIndentedString(meta);
            Console.WriteLine($"Generated JSON: {json}");
            return (json, meta);
        }
        
        public (string json, TMeta meta) GenerateWithoutBuilderType(TData? data)
        {
            var dummyKnownBuilderType = DraftBuilderType.Fns.BusinessRegistration.Registration;
            var meta = metaGenerator.GenerateWithoutData(dummyKnownBuilderType);
            meta.BuilderType = null;
            meta.BuilderData = data!;
            var json = serializer.SerializeToIndentedString(meta);
            Console.WriteLine($"Generated JSON: {json}");
            return (json, meta);
        }

        public TMeta Deserialize(string json) => serializer.Deserialize<TMeta>(json);
    }
}